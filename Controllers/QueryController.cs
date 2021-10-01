using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.IO;

using IoTConsoleAPI._Services.Interfaces;
using IoTConsoleAPI.Data.DTO;
using IoTConsoleAPI.Data.Models;
using IoTConsoleAPI.Helpers;

namespace IoTConsoleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : ControllerBase
    {
        private readonly IQueryService _queryService;
        public QueryController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        [HttpPost("search")]
        public async Task<IActionResult> TemperatureDataSearch(JToken jtk)
        {            
            var dateRange = jtk.Value<JObject>("rangeParam").ToObject<DateRange>();
            var locationId = jtk.Value<string>("locationParam").ToString();

            var lists = await _queryService.SearchTemperatureData(dateRange, locationId);
            return Ok(lists);
        }

        [HttpPost("report")]
        public async Task<IActionResult> ExportData(JToken jtk) 
        {
            var stream = new MemoryStream(); 
            var dateRange = jtk.Value<JObject>("rangeParam").ToObject<DateRange>();
            var locationId = jtk.Value<string>("locationParam").ToString();

            var data = await _queryService.SearchTemperatureData(dateRange, locationId);
            var locationname = data.Count > 0 ? data[0].LocationName : "Invalid";
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Temperarture Data");
                workSheet.Column(1).Width = 21;
                workSheet.Column(2).Width = 14;
                workSheet.Cells["A1"].Value = "Temperature & Humidity";
                workSheet.Cells["A2"].Value = dateRange.StartDate.ToLocalTime().ToString() + " - " + dateRange.EndDate.ToLocalTime().ToString();
                workSheet.Cells["A3"].Value = locationname; 
                workSheet.Cells["A4"].Value = "Date Time"; 
                workSheet.Cells["B4"].Value = "Temperature";
                workSheet.Cells["C4"].Value = "Humidity"; 
                workSheet.Cells["A4:C4"].Style.Font.Bold = true;
                int row = 5;
                foreach ( var item in data)
                {
                    workSheet.Cells[row, 1].Value = item.InsertAt;
                    workSheet.Cells[row, 1].Style.Numberformat.Format = "yyyy/mm/dd hh:MM:ss";
                    workSheet.Cells[row, 2].Value = item.Temperature;
                    workSheet.Cells[row, 3].Value = item.Humidity;
                    row++;
                }
                package.Save();
            }
            stream.Position = 0;  
            string excelName = $"TemperatureData-{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx";  
        
            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/xlsx" , excelName);
        
        }
    }
}