using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

namespace ComeTogether.Domain.Entities
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [StringLength(230, ErrorMessage = "Max length is 30 symbols")]
        public string FullName { get; set; }
        public string Email { get; set; }

        public decimal SalaryPerHour { get; set; }
        public decimal WorkingTime { get; set; }
        public decimal Salary { get; set; }
        public decimal Phone { get; set; }
        public decimal TaxSize { get; set; }
        public bool IncludeTax { get; set; }
        public bool SendOnEmail { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public DateTime GeneratingDate { get; set; }
    }



    public class Report
    {
        public string ProjectName { get; set; }
        public decimal CurrentBudget { get; set; }
        public DateTime GeneratingDate { get; set; }
    }


    public class Bill
    {
        public string ClientName { get; set; }
        public int BillVal { get; set; }
        public DateTime Date { get; set; }
    }

    public class Expenses
    {
        public string ClientName { get; set; }
        public int Marketing_expenses { get; set; }
        public int Clients_count { get; set; }
        public int expenses { get; set; }
        public DateTime Date { get; set; }
    }


    public class ClientsCountPerYear
    {
        public string ClientName { get; set; }
        public int _2023 { get; set; }

        public int _2022 { get; set; }

        public int _2021 { get; set; }
        public int Date { get; set; }
    }

    public class BenefitInYear
    {

        public int profit { get; set; }
        public DateTime Date { get; set; }

    }

    public class BenefitInTheEndOfMonth
    {

        public string ClientName { get; set; }

        public string fields { get; set; }
        public int benefit2021 { get; set; }
        public int aDate { get; set; }
        public int Day { get; set; }
        public int Bill { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }


    public class BudgetWithTaxesAndCharity
    {

        public string ProjectName { get; set; }

        public int Initial_budget { get; set; }
        public int FullCharity { get; set; }
        public int FullBill { get; set; }
        public int FullDutyMoney { get; set; }
        public int Result_For_This_Month { get; set; }
    }
    public class notEnoughtClients
    {
        public string ProjectName { get; set; }

        public int ProjectId { get; set; }
    }
    public class hasSponsoring
    {
        public string hasSponsor;

        public int Initial_budget { get; set; }
        public int ProjectId { get; set; }
    }
}
