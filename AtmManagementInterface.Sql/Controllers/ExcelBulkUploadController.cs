using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Threading;
using OfficeOpenXml;
using AtmManagementInterface.Data.ModelContext;
using AtmManagementInterface.Sql.Models;

namespace ATM_Management_Interface.Controllers
{
    public class ExcelBulkUploadController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        CustomerRegistrationContext _db = new CustomerRegistrationContext();

        public ExcelBulkUploadController(IWebHostEnvironment hostingEnvironment)

        {
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: ExcelBulkUploadController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ExcelBulkUploadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExcelBulkUploadController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExcelBulkUploadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExcelBulkUploadController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExcelBulkUploadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult createFile()
        {
            try
            {
                string wwwrootPath = _hostingEnvironment.WebRootPath;
                fileName = @"EjBulkUpload.xlsx";


                return downloadFile(wwwrootPath);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private string fileName { get; set; }


        public FileResult downloadFile(string filePath)
        {
            IFileProvider provider = new PhysicalFileProvider(filePath);
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            var mimeType = "application/vnd.ms-excel";
            return File(readStream, mimeType, fileName);
        }



        [HttpGet]
        public async Task<FileResult> Download()
        {


            string fileName;
            fileName = "EjBulkUpload.xlsx";


            var path = Path.Combine(
               Directory.GetCurrentDirectory(),
               "wwwroot\\", fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, MediaTypeNames.Application.Octet, Path.GetFileName(path));

        }
        public async Task<IActionResult> Downloads(string filename)
        {
            filename = "EjBulkUpload.xlsx";
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            try
            {
                var types = GetMimeTypes();
                var ext = Path.GetExtension(path).ToLowerInvariant();
                return types[ext];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private Dictionary<string, string> GetMimeTypes()
        {
            try
            {
                return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                 {".csv", "text/csv"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                //{".pdf", "application/pdf"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats"},
                //           officedocument.spreadsheetml.sheet"},
                //{".png", "image/png"},
                //{".jpg", "image/jpeg"},
                //{".jpeg", "image/jpeg"},
                //{".gif", "image/gif"},
               
            };
            }
            catch (Exception ex)
            {         
              
                throw (ex);
            }
        }
        public JsonResult GetExistingRecord()
        {
            var reply = _db.TblEjfileUploadedDocumentTemp.Count();
            return Json(reply);
        }

        public JsonResult listGeneralupload()
        {
            var result = _db.TblEjfileUploadedDocumentTemp.ToList();
            return Json(result);
        }

        public IActionResult RemoveBulk(TblEjfileUploadedDocumentTemp TransferUpload)
        {
            try
            {
                _db.TblEjfileUploadedDocumentTemp.Remove(TransferUpload);
                _db.SaveChanges();

                return Json(TransferUpload.Id);
            }
            catch (Exception ex)
            {              
                throw (ex);
            }
        }


        [HttpPost]
        public async Task<JsonResult> Import(IFormFile formFile, UploadTempplate upload, CancellationToken cancellationToken)
        {
            try
            {  
                formFile = upload.Staffsignature.FirstOrDefault();

                var extension = "." + formFile.FileName.Split('.')[formFile.FileName.Split('.').Length - 1];
                string fileName = DateTime.Now.Ticks + extension;

                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files",
                   fileName);

                var list = new List<TblEjfileUploadedDocumentTemp>();        
                var countResult = 0;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                    //var getMiscode = AuthenticateMISCODE().ToList();

                    TblEjfileUploadedDocumentTemp temp = new TblEjfileUploadedDocumentTemp();
                    var list2 = new List<TblEjfileUploadedDocumentTemp>();
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        //ExcelWorksheet worksheet = package.Workbook.Worksheets["EjBulkUpload"];
                        if (worksheet == null)
                        {

                            return Json(new { message = "The sheet name has been changed" });
                        }
                        DateTime transdate;
                        var rowCount = worksheet.Dimension.Rows;
                        var columnCount = worksheet.Dimension.Columns;
                        for (int row = 2; row <= rowCount; row++)
                        {        
                         


                            countResult = list2.Count();
                            if (countResult == 0)
                            {

                                list.Add(new TblEjfileUploadedDocumentTemp
                                {

                                    AtmId = worksheet.Cells[row, 1].Value.ToString(),
                                    Brand = worksheet.Cells[row, 2].Value.ToString(),
                                   // TransactionDate = Convert.ToDateTime(worksheet.Cells[row, 3].Value, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat),
                                    //Time = (TimeSpan?)(worksheet.Cells[row, 4].Value),
                                    Tns = worksheet.Cells[row, 5].Value.ToString(),
                                    Pan = worksheet.Cells[row, 6].Value.ToString(),
                                    TransactionType = Convert.ToString(worksheet.Cells[row, 7].Value),
                                    Currency = worksheet.Cells[row, 8].Value.ToString(),
                                    Amount = Convert.ToDecimal(worksheet.Cells[row, 9].Value),
                                    AvailableAmt = Convert.ToDecimal(worksheet.Cells[row, 10].Value),
                                    LedgerAmt = Convert.ToDecimal(worksheet.Cells[row, 11].Value),
                                    SurCharge = Convert.ToDecimal(worksheet.Cells[row, 12].Value),
                                    SourceAcct = Convert.ToString(worksheet.Cells[row, 13].Value),
                                    DestinationAcct = Convert.ToString(worksheet.Cells[row, 14].Value),
                                    Comment = Convert.ToString(worksheet.Cells[row, 15].Value)
                        


                                }); 
                            }


                        }
                        if (countResult > 0)
                        {                          

                            return Json(new { message = list2 });
                        }
                    }

                    _db.TblEjfileUploadedDocumentTemp.AddRange(list);
                    _db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
  
                return Json(ex);
            }


            return Json(new { message = "" });
        }

        [HttpPost]
        public JsonResult batchuploadTransfer(List<TblEjfileUploadedDocumentTemp> batchData, TblEjfileUploadedDocumentTemp BatchTransfer)
        {
            try
            {
                int result = 0;
                foreach (var temp in batchData)
                {
                    TblEjfileUploadedDocument transferUpload = new TblEjfileUploadedDocument
                    {
                        AtmId = temp.AtmId,
                        //Accountnodr = casa,

                        Brand = temp.Brand,
                        PostedBy = string.Empty,
                        DatePosted = DateTime.Now,
                        Tns = temp.Tns,
                        AvailableAmt = temp.AvailableAmt,
                        LedgerAmt = temp.LedgerAmt,
                        SurCharge = temp.SurCharge,
                        DestinationAcct = temp.DestinationAcct,
                        SourceAcct = temp.SourceAcct,
                        Currency = temp.Currency,
                        Time = temp.Time,
                        TransactionDate = temp.TransactionDate,
                        TransactionType = temp.TransactionType,
                        Amount = temp.Amount,
                        Pan = temp.Pan,
                        Comment = temp.Comment

                    };
                  
                    _db.TblEjfileUploadedDocument.Add(transferUpload);
                    result = _db.SaveChanges();

                    var UploadTemp = _db.TblEjfileUploadedDocumentTemp.Where(o => o.AtmId == temp.AtmId).FirstOrDefault();

                    _db.TblEjfileUploadedDocumentTemp.Remove(UploadTemp);
                    _db.SaveChanges();

                }
                if (result == 1)
                { 
                    return Json(new { message = " " });

                }
                else
                {
                    return Json(new { message = "The transaction failed to process." });
                }
            }
            catch (Exception ex)
            {         
                return Json(ex);
            }
        }


    }
}
