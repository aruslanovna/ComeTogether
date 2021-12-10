using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComeTogether.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Deals = new HashSet<Deal>();
        }

        public byte[] Photo { get; set; }

        public string WorkPlace { get; set; }
        public string Position { get; set; }
        public string Info { get; set; }


        public string LastName { get; set; }


        public DateTime? BirthDate { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }

        public MainNacel? MainNacel { get; set; }
        public string Country { get; set; }

        public int? Privilage{ get; set; }
        public ICollection<Deal> Deals{ get; set; }
        public ICollection<BusinessRegister> BusinessRegisters { get; set; }
        // public List<AuthenticationScheme> ExternalLogins { get; set; }
        // public string ReturnUrl { get; set; }
        // public List<int> foundedPartnerId { get; set; }
        // public List<int> joinedPartnerId { get; set; }


    }

    public enum MainNacel
    {
        A,B,C,D,E,F, G,

H  , 
I ,  
J  , 
K  , 
L  ,
M   ,
N  ,
O  ,
P  ,
Q  ,
R  ,
S   ,
T   ,
U
        // "Сільське господарство, лісове господарство та рибне господарство"

        //"Добувна промисловість і розроблення кар'єрів"

        // "Переробна промисловість"

        // "Постачання електроенергії, газу, пари та кондиційованого повітря"

        // "Водопостачання; каналізація, поводження з відходами"

        // "Будівництво"

        //"Оптова та роздрібна торгівля; ремонт автотранспортних засобів і мотоциклів"

        //"Транспорт, складське господарство, поштова та кур'єрська діяльність"

        // "Тимчасове розміщування й організація харчування"

        //         "Інформація та телекомунікації"

        //        "Фінансова та страхова діяльність"

        //   "Операції з нерухомим майном"

        //  "Професійна, наукова та технічна діяльність"

        //  "Діяльність у сфері адміністративного та допоміжного обслуговування"

        //  "Державне управління й оборона; обов'язкове соціальне страхування"

        //  "Освіта"

        // "Охорона здоров'я та надання соціальної допомоги"

        // "Мистецтво, спорт, розваги та відпочинок"

        // "Надання інших видів послуг"

        // "Діяльність домашніх господарств"

        // "Діяльність екстериторіальних організацій і органів"


    }
}
