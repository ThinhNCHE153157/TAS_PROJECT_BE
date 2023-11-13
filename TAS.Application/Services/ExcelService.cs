using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using TAS.Infrastructure.Constants;
using TAS.Application.Services.Interfaces;
using AutoMapper;
using TAS.Data.EF;

namespace TAS.Application.Services
{
	public class ExcelService : IExcelService
	{
		private readonly IUnitOfWork _unitOfWork;
		public readonly IMapper _mapper;

		private static string sheetName = "Account";
		public static DataTable accountExcel(List<Account> listAccount)
		{
			DataTable dt = new DataTable(sheetName);
			dt.Columns.AddRange(new DataColumn[9]{
				new DataColumn("First Name"),
				new DataColumn("Last Name"),
				new DataColumn("Email"),
				new DataColumn("Phone"),
				new DataColumn("Address"),
				new DataColumn("Create User"),
				new DataColumn("Update User"),
				new DataColumn("Created Date"),
				new DataColumn("Updated Date"),
			});

			foreach (var o in listAccount)
			{
				dt.Rows.Add(
					o.FirstName,
					o.LastName,
					o.Email,
					o.Phone,
					o.Address,
					o.CreateUser,
					o.UpdateUser,
					o.CreateDate,
					o.UpdateDate);
			}
			return dt;
		}

		/*public static DataTable sampleTemplateAccount()
		{
			DataTable dt = new DataTable(sheetName);
			dt.Columns.AddRange(new DataColumn[9]{
				new DataColumn("First name"),
				new DataColumn("Mid name"),
				new DataColumn("Last name"),
				new DataColumn("Date of birth"),
				new DataColumn("Gender"),
				new DataColumn("Address"),
				new DataColumn("Phone"),
				new DataColumn("Major"),
				new DataColumn("Note"),
			});
			return dt;
		}*/


		public static List<Account> readData(IFormFile file)
		{
			try
			{
				using var workbook = new XLWorkbook(file.OpenReadStream());
				var ws = workbook.Worksheet(1);
				List<Account> accounts = new List<Account>();

				foreach (IXLRow row in ws.RowsUsed().Skip(1))
				{
					var check = Common.checkStringsIsNullOrEmpty(new string[]
					{
						row.Cell(1).Value.ToString(),
						row.Cell(2).Value.ToString(),
						row.Cell(3).Value.ToString(),
						row.Cell(4).Value.ToString(),
						row.Cell(5).Value.ToString(),
					});
					if (check)
					{
						throw new NullReferenceException("Check the information again!");
					}
					var o = new Account()
					{
						AccountId = 0,
						FirstName = row.Cell(1).Value.ToString(),
						LastName = row.Cell(2).Value.ToString(),
						Email = row.Cell(3).Value.ToString(),
						Phone = row.Cell(4).Value.IsNumber ? row.Cell(4).Value.ToString() : null,
						Address = row.Cell(5).Value.ToString(),
						CreateUser = row.Cell(6).Value.ToString(),
						UpdateUser = row.Cell(7).Value.IsBlank ? null : row.Cell(7).Value.ToString(),
						CreateDate = row.Cell(8).Value.IsDateTime ? 
						DateTime.Parse(row.Cell(8).Value.ToString()) : throw new NullReferenceException("Check again!"),
						UpdateDate = row.Cell(9).Value.IsDateTime ?
						DateTime.Parse(row.Cell(9).Value.ToString()) : throw new NullReferenceException("Check again!"),
					};
					accounts.Add(o);
				}
				return accounts;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			
		}
	}
}
