using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAS.Application.Services;
using TAS.Application.Services.Interfaces;
using ClosedXML.Excel;

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

	}
}
