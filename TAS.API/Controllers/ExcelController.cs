using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAS.Application.Services;
using TAS.Application.Services.Interfaces;
using ClosedXML.Excel;
using TAS.Data.Dtos.Responses;
using TAS.Data.Dtos.Requests;
using System.Net;

namespace TAS.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExcelController : ControllerBase
	{
		private IMapper mapper;
		//private ExcelService excelService;
		private readonly IAccountService _accountService;
		private readonly ITokenService _tokenService;

		public ExcelController(IAccountService accountService, ITokenService tokenService)
		{
			_accountService = accountService;
			_tokenService = tokenService;
		}

	

		[HttpGet("ExportAccountToExcel")]
		public async Task<IActionResult> ExportAccountToExcel()
		{
			string fileName = "testexcel.xlsx";

			// Await the asynchronous operation
			var accounts = await _accountService.GetAllAccounts();

			var dt = ExcelService.accountExcel(accounts);

			try
			{
				using (XLWorkbook wb = new XLWorkbook())
				{
					wb.Worksheets.Add(dt);
					using (MemoryStream stream = new MemoryStream())
					{
						wb.ColumnWidth = 25;
						wb.SaveAs(stream);
						return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
					}
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpGet("templateAccountExcel")]
		public IActionResult ExportTemplateExcel()
		{
			string fileName = "template-account.xlsx";
			var dt = ExcelService.sampleTemplateAccount();
			try
			{
				using (XLWorkbook wb = new XLWorkbook())
				{
					wb.Worksheets.Add(dt);
					using (MemoryStream stream = new MemoryStream())
					{
						wb.SaveAs(stream);
						return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
					}
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpPost]
		[Route("importAccount")]

		public ExcelResponseBodyRequest<List<AccountHomepageResponeDTO>> ReadExcel([FromForm] IFormFile file)
		{
			try
			{
				var rs = ExcelService.readDataAccount(file);
				return new ExcelResponseBodyRequest<List<AccountHomepageResponeDTO>>()
				{
					code = HttpStatusCode.OK,
					message = "SuccessFully!",
					data = mapper.Map<List<AccountHomepageResponeDTO>>(rs),
				};
			}
			catch(Exception ex)
			{
				return new ExcelResponseBodyRequest<List<AccountHomepageResponeDTO>>()
				{
					code = HttpStatusCode.NotAcceptable,
					message = ex.Message,
				};
			}
		}
	}
}
