using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//
using CsvContentHelper;
using CsvContentHelper.Models;
using CSV_DynamicParser.Models;
using CSV_DynamicParser.ViewModels;

namespace CSV_DynamicParser.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// to input csv file to map
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            viewModel.FileHasHeaders = false;
            viewModel.MappedItems = GetMappedDataSet(0);
            viewModel.DataSet = new List<string[]>();
            viewModel.MappingFieldList = GetExampleConfigurationDataSet();

            return View(viewModel);
        }
        /// <summary>
        /// Show mapped data set
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            model.MappingFieldList = GetExampleConfigurationDataSet();
            var csvHelper = new CsvHelper();
            var dict = csvHelper.GetRowData(model.CsvFile, model.FileHasHeaders);
            model.DataSet = dict;

            return View("Result", model);
        }
        /// <summary>
        /// upload csv file to server to map.
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="hasHeader"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Upload(string fileContent, bool hasHeader)
        {
            const int MaxCharsInput = 800;
            var viewModel = new IndexViewModel();
            var csvHelper = new CsvHelper();
            if (csvHelper.CheckCsvFileLength(fileContent, MaxCharsInput) == true)
            {
                if(csvHelper.CheckCsvFileContent(fileContent) == true)
                {
                    var fields = csvHelper.GetMappingFields(fileContent, hasHeader, MaxCharsInput);
                    var dict = csvHelper.GetRowData(fileContent, hasHeader, MaxCharsInput);
                    // 
                    viewModel.FileHasHeaders = hasHeader;
                    if (hasHeader == true)
                    {
                        viewModel.FileHeaderCount = fields.Count;
                    }
                    else
                    {
                        viewModel.FileHeaderCount = 0;
                    }
                    viewModel.MappedItems = fields;
                    viewModel.DataSet = dict;
                    viewModel.ErrorMessage = string.Empty;
                }
                else
                {
                    viewModel.ErrorMessage = "Csv Input File content is not correct, please have a check on the file uploaded.";
                }
            }
            else
            {
                viewModel.ErrorMessage = string.Concat("Csv Input File Contains too much characters, over than ", MaxCharsInput, "!");
            }
            viewModel.MappingFieldList = GetExampleConfigurationDataSet();

            return Json(viewModel); 
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<MappingStructure> GetMappedDataSet(int count = 4)
        {
            var list = new List<MappingStructure>();
            for(var i = 0; i < count; i++)
            {
                var item = new MappingStructure
                {
                    CsvFileHeader = "Product Name",
                    Order = i,
                    MappedField = "Title"
                };
                list.Add(item);
            }

            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private IEnumerable<ConfigurationData> GetExampleConfigurationDataSet(int count = 6)
        {
            var list = new List<ConfigurationData>();
            #region // get ready for example configuration data...
            var titleItem = new ConfigurationData
            {
                Name = "Title",
                DataType = DataTypeEnum.String,
                Required = true,
                MaxLength = 20
            };
            list.Add(titleItem);
            // 
            var DescriptionItem = new ConfigurationData
            {
                Name = "Description",
                DataType = DataTypeEnum.String,
                Required = false,
                MaxLength = 60
            };
            list.Add(DescriptionItem);
            // 
            var PriceItem = new ConfigurationData
            {
                Name = "Price",
                DataType = DataTypeEnum.Decimal,
                Required = true,
                MaxLength = 8
            };
            list.Add(PriceItem);
            // 
            var BrandItem = new ConfigurationData
            {
                Name = "Brand",
                DataType = DataTypeEnum.String,
                Required = true,
                MaxLength = 15
            };
            list.Add(BrandItem);
            // 
            var MPNItem = new ConfigurationData
            {
                Name = "MPN",
                DataType = DataTypeEnum.String,
                Required = true,
                MaxLength = 20
            };
            list.Add(MPNItem);
            // 
            var CategoryItem = new ConfigurationData
            {
                Name = "Category",
                DataType = DataTypeEnum.Integer,
                Required = true,
                MaxLength = 3
            };
            list.Add(CategoryItem);
            #endregion 

            return list;
        }
    }
}
