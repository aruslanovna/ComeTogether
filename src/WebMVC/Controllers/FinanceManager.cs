using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure;
using ComeTogether.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Controllers
{
    public class FinanceManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IConfiguration configuration;


        public FinanceManagerController(ApplicationDbContext context, IConfiguration config)
        {

            configuration = config;
            _context = context;
        }


        // GET: Categories
        public async Task<IActionResult> Index(string searchString)
        {

            return View();
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = _context.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            //.FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult InvoiceStep4(Invoice invoice)
        {
            Invoice _invoice = new Invoice();
            _invoice.Salary = _invoice.SalaryPerHour * _invoice.WorkingTime;
            return View(_invoice);
        }


        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateInvoice(Invoice invoice)
        {

            invoice.Salary = invoice.SalaryPerHour * invoice.WorkingTime;
            if (invoice.IncludeTax)
            {
                invoice.Salary -= invoice.TaxSize;
            }


            return RedirectToAction(nameof(InvoiceStep4), invoice);
        }

        // POST: Categories/Delete/5

        public async Task<IActionResult> CurrentBudget2(int projectId)
        {



            string cnnString = configuration.GetConnectionString("ApplicationConnection");

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "CurrentBudget";


            IDbDataParameter date = cmd.CreateParameter();
            date.ParameterName = "@Date";
            date.Direction = System.Data.ParameterDirection.Output;
            date.DbType = System.Data.DbType.Date;
            cmd.Parameters.Add(date);

            IDbDataParameter projectName = cmd.CreateParameter();
            projectName.ParameterName = "@ProjectName";
            projectName.Direction = System.Data.ParameterDirection.Output;
            projectName.DbType = System.Data.DbType.String;
            projectName.Size = 230;
            cmd.Parameters.Add(projectName);

            IDbDataParameter currentBudget = cmd.CreateParameter();
            currentBudget.ParameterName = "@current_money_in_cafe";
            currentBudget.Direction = System.Data.ParameterDirection.Output;
            currentBudget.DbType = System.Data.DbType.Decimal;
            cmd.Parameters.Add(currentBudget);

            //  cmd.Parameters.Add("@current_money_in_cafe", SqlDbType.Int).Direction = ParameterDirection.Output;
            //add any parameters the stored procedure might require
            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();

            Report rep = new Report();
            rep.CurrentBudget = (decimal)currentBudget.Value;
            rep.ProjectName = (string)projectName.Value;
            rep.GeneratingDate = (DateTime)date.Value;

            return View(rep);
        }



        public async Task<IActionResult> Bill2(string userId)
        {



            string cnnString = configuration.GetConnectionString("ApplicationConnection");

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "calculate_bill";


            IDbDataParameter date = cmd.CreateParameter();
            date.ParameterName = "@paramId";
            date.Value = userId;
            date.Direction = System.Data.ParameterDirection.Input;
            date.DbType = System.Data.DbType.String;
            cmd.Parameters.Add(date);

            IDbDataParameter projectName = cmd.CreateParameter();
            projectName.ParameterName = "@profit";
            projectName.Direction = System.Data.ParameterDirection.Output;
            projectName.DbType = System.Data.DbType.Int32;

            cmd.Parameters.Add(projectName);



            //  cmd.Parameters.Add("@current_money_in_cafe", SqlDbType.Int).Direction = ParameterDirection.Output;
            //add any parameters the stored procedure might require
            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();

            Bill rep = new Bill();
            rep.ClientName = _context.Clients.Where(x => x.UserId == userId).Select(x => x.Name).FirstOrDefault();
            rep.BillVal = (int)projectName.Value;
            rep.Date = _context.Clients.Where(x => x.UserId == userId).Select(x => x.Date).FirstOrDefault(); ;

            return View(rep);
        }


        public async Task<IActionResult> Expenses2()
        {


            string cnnString = configuration.GetConnectionString("ApplicationConnection");

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "moneyPerClient";




            IDbDataParameter Date = cmd.CreateParameter();
            Date.ParameterName = "@Date";
            Date.Direction = System.Data.ParameterDirection.Output;
            Date.DbType = System.Data.DbType.DateTime;
            cmd.Parameters.Add(Date);



            IDbDataParameter Marketing_expenses = cmd.CreateParameter();
            Marketing_expenses.ParameterName = "@Marketing_expenses";
            Marketing_expenses.Direction = System.Data.ParameterDirection.Output;
            Marketing_expenses.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(Marketing_expenses);

            IDbDataParameter Clients_count = cmd.CreateParameter();
            Clients_count.ParameterName = "@Clients_count";
            Clients_count.Direction = System.Data.ParameterDirection.Output;
            Clients_count.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(Clients_count);

            IDbDataParameter expenses = cmd.CreateParameter();
            expenses.ParameterName = "@expenses";
            expenses.Direction = System.Data.ParameterDirection.Output;
            expenses.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(expenses);

            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();

            Expenses rep = new Expenses();

            rep.Date = (DateTime)Date.Value;
            rep.Clients_count = (int)Clients_count.Value;
            rep.expenses = (int)expenses.Value;
            rep.Marketing_expenses = (int)Marketing_expenses.Value;

            return View(rep);
        }

        public async Task<IActionResult> ClientsCountPerYear2()
        {


            string cnnString = configuration.GetConnectionString("ApplicationConnection");

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "ClientsCountPerYear";




            IDbDataParameter Date = cmd.CreateParameter();
            Date.ParameterName = "@Date";
            Date.Direction = System.Data.ParameterDirection.Output;
            Date.DbType = System.Data.DbType.DateTime;
            cmd.Parameters.Add(Date);



            IDbDataParameter _2021 = cmd.CreateParameter();
            _2021.ParameterName = "@2021";
            _2021.Direction = System.Data.ParameterDirection.Output;
            _2021.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(_2021);

            IDbDataParameter _2022 = cmd.CreateParameter();
            _2022.ParameterName = "@2022";
            _2022.Direction = System.Data.ParameterDirection.Output;
            _2022.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(_2022);

            IDbDataParameter _2023 = cmd.CreateParameter();
            _2023.ParameterName = "@2023";
            _2023.Direction = System.Data.ParameterDirection.Output;
            _2023.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(_2023);

            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();

            ClientsCountPerYear rep = new ClientsCountPerYear();
            rep.Date = (int)Date.Value;
            rep._2021 = (int)_2021.Value;
            rep._2022 = (int)_2022.Value;
            rep._2023 = (int)_2023.Value;

            return View(rep);
        }


        public async Task<IActionResult> BenefitInYear2()
        {


            string cnnString = configuration.GetConnectionString("ApplicationConnection");

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "BenefitInYear";



            IDbDataParameter profit = cmd.CreateParameter();
            profit.ParameterName = "@profit";
            profit.Direction = System.Data.ParameterDirection.Output;
            profit.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(profit);

            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();


            BenefitInYear rep = new BenefitInYear();
            rep.Date = DateTime.Now;
            rep.profit = (int)profit.Value;


            return View(rep);
        }

        public async Task<IActionResult> notEnoughtClients2()
        {
            string cnnString = configuration.GetConnectionString("ApplicationConnection");

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "notEnoughtClients";




            IDbDataParameter ProjectId = cmd.CreateParameter();
            ProjectId.ParameterName = "@ProjectId";
            ProjectId.Direction = System.Data.ParameterDirection.Output;
            ProjectId.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(ProjectId);

            IDbDataParameter ProjectName = cmd.CreateParameter();
            ProjectName.ParameterName = "@ProjectName";
            ProjectName.Direction = System.Data.ParameterDirection.Output;
            ProjectName.DbType = System.Data.DbType.String;
            ProjectName.Size = 230;
            cmd.Parameters.Add(ProjectName);

            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();

            notEnoughtClients rep = new notEnoughtClients();
            rep.ProjectName = (string)ProjectName.Value;
            rep.ProjectId = (int)ProjectId.Value;

            return View(rep);
        }

        public async Task<IActionResult> BudgetWithTaxesAndCharity2()
        {

            string cnnString = configuration.GetConnectionString("ApplicationConnection");

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "BudgetWithTaxesAndCharity";




            IDbDataParameter Initial_budget = cmd.CreateParameter();
            Initial_budget.ParameterName = "@Initial_budget";
            Initial_budget.Direction = System.Data.ParameterDirection.Output;
            Initial_budget.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(Initial_budget);

            IDbDataParameter ProjectName = cmd.CreateParameter();
            ProjectName.ParameterName = "@ProjectName";
            ProjectName.Direction = System.Data.ParameterDirection.Output;
            ProjectName.DbType = System.Data.DbType.String;
            ProjectName.Size = 230;
            cmd.Parameters.Add(ProjectName);


            IDbDataParameter FullCharity = cmd.CreateParameter();
            FullCharity.ParameterName = "@FullCharity";
            FullCharity.Direction = System.Data.ParameterDirection.Output;
            FullCharity.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(FullCharity);


            IDbDataParameter FullBill = cmd.CreateParameter();
            FullBill.ParameterName = "@FullBill";
            FullBill.Direction = System.Data.ParameterDirection.Output;
            FullBill.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(FullBill);


            IDbDataParameter FullDutyMoney = cmd.CreateParameter();
            FullDutyMoney.ParameterName = "@FullDutyMoney";
            FullDutyMoney.Direction = System.Data.ParameterDirection.Output;
            FullDutyMoney.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(FullDutyMoney);


            IDbDataParameter Result_For_This_Month = cmd.CreateParameter();
            Result_For_This_Month.ParameterName = "@Result_For_This_Month";
            Result_For_This_Month.Direction = System.Data.ParameterDirection.Output;
            Result_For_This_Month.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(Result_For_This_Month);

            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();

            BudgetWithTaxesAndCharity rep = new BudgetWithTaxesAndCharity();
            rep.ProjectName = (string)ProjectName.Value;
            rep.FullBill = (int)FullBill.Value;
            rep.FullCharity = (int)FullCharity.Value;
            rep.FullDutyMoney = (int)FullDutyMoney.Value;
            rep.Initial_budget = (int)Initial_budget.Value;
            rep.Result_For_This_Month = (int)Result_For_This_Month.Value;
            return View(rep);
        }

        public async Task<IActionResult> BenefitInTheEndOfMonth2()
        {

            string cnnString = configuration.GetConnectionString("ApplicationConnection");

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "benefitin2022";




            IDbDataParameter benefit2021 = cmd.CreateParameter();
            benefit2021.ParameterName = "@Bill";
            benefit2021.Direction = System.Data.ParameterDirection.Output;
            benefit2021.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(benefit2021);

            IDbDataParameter ClientName = cmd.CreateParameter();
            ClientName.ParameterName = "@ClientName";
            ClientName.Direction = System.Data.ParameterDirection.Output;
            ClientName.DbType = System.Data.DbType.String;
            ClientName.Size = 230;
            cmd.Parameters.Add(ClientName);


            IDbDataParameter Year = cmd.CreateParameter();
            Year.ParameterName = "@Year";
            Year.Direction = System.Data.ParameterDirection.Output;
            Year.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(Year);


            IDbDataParameter Day = cmd.CreateParameter();
            Day.ParameterName = "@Day";
            Day.Direction = System.Data.ParameterDirection.Output;
            Day.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(Day);


            IDbDataParameter Month = cmd.CreateParameter();
            Month.ParameterName = "@Month";
            Month.Direction = System.Data.ParameterDirection.Output;
            Month.DbType = System.Data.DbType.Int32;
            cmd.Parameters.Add(Month);

            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();

            BenefitInTheEndOfMonth rep = new BenefitInTheEndOfMonth();
            rep.Day = (int)Day.Value;
            rep.Bill = (int)benefit2021.Value;
            rep.ClientName = (string)ClientName.Value;
            rep.Month = (int)Month.Value;
            rep.Year = (int)Year.Value;

            return View(rep);
        }

        public IActionResult BenefitInTheEndOfMonth()
        {
            string cnnString = configuration.GetConnectionString("ApplicationConnection");

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlConnection conn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.benefit_in_2022()", conn);
            // cmd.CommandType=CommandType.StoredProcedure;  
            // cmd.Parameters.AddWithValue("@username", "username");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<BenefitInTheEndOfMonth> list= new List<BenefitInTheEndOfMonth>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BenefitInTheEndOfMonth rep = new BenefitInTheEndOfMonth();
                rep.Day = (int)dt.Rows[i][4];
                rep.Bill = (int)dt.Rows[i][1];
                rep.ClientName = (string)dt.Rows[i][0];
                rep.Month = (int)dt.Rows[i][3];
                rep.Year = (int)dt.Rows[i][2];
                list.Add(rep);
                string str = dt.Rows[i][0].ToString();
            }
           return View(list);
        }


        public async Task<IActionResult> Bill(string userId)
        {
            string cnnString = configuration.GetConnectionString("ApplicationConnection");
            SqlConnection conn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.calculate_bill('{userId}')", conn);
            // cmd.CommandType=CommandType.StoredProcedure;  
             //cmd.Parameters.AddWithValue("@userId", "userId");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Bill> list = new List<Bill>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Bill rep = new Bill();
                
               
                rep.BillVal = (int)dt.Rows[i][0];
                rep.ClientName = _context.Clients.Where(x => x.UserId == userId).Select(x => x.Name).FirstOrDefault();
               
                rep.Date = _context.Clients.Where(x => x.UserId == userId).Select(x => x.Date).FirstOrDefault(); ;
                list.Add(rep);
              
            }
            return View(list);
        }


        public async Task<IActionResult> Expenses()
        {

            string cnnString = configuration.GetConnectionString("ApplicationConnection");
            SqlConnection conn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.money_Per_Client()", conn);
            // cmd.CommandType=CommandType.StoredProcedure;  
            // cmd.Parameters.AddWithValue("@username", "username");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Expenses> list = new List<Expenses>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               

               

        Expenses rep = new Expenses();

                rep.Date = (DateTime)dt.Rows[i][0];
                rep.Clients_count = (int)dt.Rows[i][2];
                rep.expenses = (int)dt.Rows[i][3];
                rep.Marketing_expenses = (int)dt.Rows[i][1];

               list.Add(rep);

            }
            return View(list);

        }

        public async Task<IActionResult> ClientsCountPerYear()
        {

            string cnnString = configuration.GetConnectionString("ApplicationConnection");
            SqlConnection conn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Clients_Count_Per_Year()", conn);
            // cmd.CommandType=CommandType.StoredProcedure;  
            // cmd.Parameters.AddWithValue("@username", "username");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<ClientsCountPerYear> list = new List<ClientsCountPerYear>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                ClientsCountPerYear rep = new ClientsCountPerYear();
                rep.Date = (int)dt.Rows[i][0];
                rep._2021 = (int)dt.Rows[i][1];
                rep._2022 = (int)dt.Rows[i][2];
                rep._2023 = (int)dt.Rows[i][3];



                list.Add(rep);

            }
            return View(list);
           

           
        }

        public async Task<IActionResult> BenefitInYear()
        {

            string cnnString = configuration.GetConnectionString("ApplicationConnection");
            SqlConnection conn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Benefit_In_Year()", conn);
            // cmd.CommandType=CommandType.StoredProcedure;  
            // cmd.Parameters.AddWithValue("@username", "username");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<BenefitInYear> list = new List<BenefitInYear>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                BenefitInYear rep = new BenefitInYear();
                rep.Date = DateTime.Now;
                rep.profit = (int)dt.Rows[i][0];
                list.Add(rep);

            }
            return View(list);

        }

        public async Task<IActionResult> notEnoughtClients()
        {

            string cnnString = configuration.GetConnectionString("ApplicationConnection");
            SqlConnection conn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.not_Enought_Clients()", conn);
            // cmd.CommandType=CommandType.StoredProcedure;  
            // cmd.Parameters.AddWithValue("@username", "username");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<notEnoughtClients> list = new List<notEnoughtClients>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                notEnoughtClients rep = new notEnoughtClients();
                rep.ProjectName = (string)dt.Rows[i][1];
                rep.ProjectId = (int)dt.Rows[i][0];

                list.Add(rep);

            }
            return View(list);


        }

        public async Task<IActionResult> BudgetWithTaxesAndCharity()
        {
            string cnnString = configuration.GetConnectionString("ApplicationConnection");
            SqlConnection conn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Budget_With_Taxes_And_Charity()", conn);
            // cmd.CommandType=CommandType.StoredProcedure;  
            // cmd.Parameters.AddWithValue("@username", "username");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<BudgetWithTaxesAndCharity> list = new List<BudgetWithTaxesAndCharity>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                BudgetWithTaxesAndCharity rep = new BudgetWithTaxesAndCharity();

                rep.ProjectName = (string)dt.Rows[i][0];
                rep.FullBill = (int)dt.Rows[i][3];
                rep.FullCharity = (int)dt.Rows[i][2];
                rep.FullDutyMoney = (int)dt.Rows[i][4];
                rep.Initial_budget = (int)dt.Rows[i][1];
                rep.Result_For_This_Month = (int)dt.Rows[i][5];
               

                list.Add(rep);

            }
            return View(list); 
        }

        public async Task<IActionResult> CurrentBudget(int projectId)
        {

            string cnnString = configuration.GetConnectionString("ApplicationConnection");
            SqlConnection conn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Current_Budget()", conn);
            // cmd.CommandType=CommandType.StoredProcedure;  
            // cmd.Parameters.AddWithValue("@username", "username");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Report> list = new List<Report>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

               
                
                Report rep = new Report();
                rep.CurrentBudget = (int)dt.Rows[i][2];
                rep.ProjectName = (string)dt.Rows[i][1];
                rep.GeneratingDate = (DateTime)dt.Rows[i][0];


                list.Add(rep);

            }
            return View(list);

        }

        public async Task<IActionResult> hasSponsoring()
        {

            string cnnString = configuration.GetConnectionString("ApplicationConnection");
            SqlConnection conn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.has_Sponsoring()", conn);
            // cmd.CommandType=CommandType.StoredProcedure;  
            // cmd.Parameters.AddWithValue("@username", "username");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<hasSponsoring> list = new List<hasSponsoring>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                hasSponsoring rep = new hasSponsoring();
                rep.Initial_budget = (int)dt.Rows[i][1];
                rep.ProjectId = (int)dt.Rows[i][0];
                rep.hasSponsor = (string)dt.Rows[i][2];

                list.Add(rep);

            }
            return View(list);

        }
    }
    
}
