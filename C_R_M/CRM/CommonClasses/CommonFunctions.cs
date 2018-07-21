using CustomerManagementSystem.BLL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CRM.CommonClasses
{
    public class CommonFunctions
    {
        private CRMContext db = null;
        public CommonFunctions(CRMContext db)
        {
            this.db = db;
        }
        public string ConvertInvalidJson(string oldJson)
        {
            string newJson = "{" + oldJson + "}";
            newJson = newJson.Replace("=", ":");

            newJson = newJson.Replace("&", ",");
            newJson = newJson.Replace("+", " ");
            newJson = newJson.Replace(":", @":""");
            newJson = newJson.Replace(",", @""",");
            newJson = newJson.Replace("}", @"""}");
            newJson = newJson.Replace("%2F", "-");
            newJson = newJson.Replace("%3A", ":");
            newJson = newJson.Replace("%20", " ");
            //int index=newJson.IndexOf(":");
            // newJson[8] = '"';
            return newJson;
        }

        public CRM.Models.UserType GetUserType(string UserName)
        {
            string query = @"select usr.id as UserID, ut.Name as Type, usr.Name as UserName from usertypes ut 
inner join Users usr on usr.UserType_Id = ut.Id
where usr.Name='{0}'";
            string parameterizedquery = string.Format(query, UserName);
            CRM.Models.UserType UserType = db.Database.SqlQuery<CRM.Models.UserType>(parameterizedquery).FirstOrDefault();
            return UserType;
        }
        public DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }


    public class ApplicationFunctions : CommonFunctions
    {
        private CRMContext db = null;

        public ApplicationFunctions(CRMContext db) : base(db)
        {
            this.db = db;
        }
        internal dynamic BindCurrencies(int CurrencyId)
        {

            var Currencies = db.Currencies.ToList();
            List<SelectListItem> CurrenciesItems = new List<SelectListItem>();
            foreach (var item in Currencies)
            {
                if (CurrencyId == item.Id)
                    CurrenciesItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = true
                    });
                else
                    CurrenciesItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                    });

            }
            return CurrenciesItems;
        }
        public List<SelectListItem> BindOperator()
        {
            var Salesmens = db.Users.Where(x => x.UserType.Id == 3).ToList();
            List<SelectListItem> SalesmenItems = new List<SelectListItem>();
            foreach (var item in Salesmens)
            {
             
                    SalesmenItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });

            }
            return SalesmenItems;

        }
        public List<SelectListItem> BindSalemen(int id)
        {
            var Salesmens = db.Users.Where(x => x.UserType.Id == 2).ToList();
            List<SelectListItem> SalesmenItems = new List<SelectListItem>();
            foreach (var item in Salesmens)
            {
                if (item.Id == id)
                    SalesmenItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = true
                    });
                else
                    SalesmenItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });

            }
            return SalesmenItems;

        }

        public List<SelectListItem> BindManager(int id)
        {
            var managers = db.Users.Where(x => x.UserType.Id == 1).ToList();
            List<SelectListItem> managersItems = new List<SelectListItem>();
            foreach (var item in managers)
            {
                if (item.Id == id)
                    managersItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = true
                    });
                else
                    managersItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });

            }
            return managersItems;

        }



        public List<SelectListItem> BindCategory(int id)
        {
            var SalesmenCategories = db.SalesmenCategories.ToList();
            List<SelectListItem> SalesmenCategoriesItems = new List<SelectListItem>();
            foreach (var item in SalesmenCategories)
            {
                if (item.Id == id)
                    SalesmenCategoriesItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = true
                    });
                else
                    SalesmenCategoriesItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });

            }
            return SalesmenCategoriesItems;

        }

        public List<SelectListItem> BindTargetPeriods(int id)
        {
            var TargetPeriods = db.TargetPeriods.ToList();
            List<SelectListItem> TargetPeriodsItems = new List<SelectListItem>();
            foreach (var item in TargetPeriods)
            {
                if (item.Id == id)
                    TargetPeriodsItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = true
                    });
                else
                    TargetPeriodsItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });

            }
            return TargetPeriodsItems;

        }



        internal List<SelectListItem> BindStatusHC(int statusId)
        {

            try
            {
                var Statuss = db.Status.ToList();
                List<SelectListItem> StatusItems = new List<SelectListItem>();
                foreach (var item in Statuss)
                {
                    if (item.Status_Id == statusId)
                        StatusItems.Add(new SelectListItem()
                        {
                            Text = item.Status1,
                            Value = item.Status_Id.ToString(),
                            Selected = true
                        });
                    else
                        StatusItems.Add(new SelectListItem()
                        {
                            Text = item.Status1,
                            Value = item.Status_Id.ToString()
                        });

                }
                return StatusItems;

                //return db.Status.Select(x => new SelectListItem()
                //{
                //    Text = x.Status1,
                //    Value = x.Status_Id.ToString()
                //}).ToList();
            }
            catch (Exception e)
            {

            }

            //List<SelectListItem> items = new List<SelectListItem>()
            //{
            //    new SelectListItem() { Text = "Meeting", Value = "Meeting" },
            //    new SelectListItem() { Text = "Demo", Value = "Demo" },
            //    new SelectListItem() { Text = "Quotation", Value = "Quotation" },

            //};
            return null;
        }



        public List<SelectListItem> BindActionTypes(int id)
        {
            var TargetTypes = db.Actions.ToList();
            List<SelectListItem> TargetTypesItems = new List<SelectListItem>();
            foreach (var item in TargetTypes)
            {
                if (item.Action_Id == id)
                    TargetTypesItems.Add(new SelectListItem()
                    {
                        Text = item.Action_Name,
                        Value = item.Action_Id.ToString(),
                        Selected = true
                    });
                else
                    TargetTypesItems.Add(new SelectListItem()
                    {
                        Text = item.Action_Name,
                        Value = item.Action_Id.ToString()
                    });

            }
            return TargetTypesItems;

        }








        public List<SelectListItem> BindTargetTypes(int id)
        {
            var TargetTypes = db.TargetTypes.ToList();
            List<SelectListItem> TargetTypesItems = new List<SelectListItem>();
            foreach (var item in TargetTypes)
            {
                if (item.Id == id)
                    TargetTypesItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = true
                    });
                else
                    TargetTypesItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });

            }
            return TargetTypesItems;

        }

        public List<SelectListItem> BindLeads(int id)
        {
            var Lead_Pools = db.Lead_Pool.ToList();
            List<SelectListItem> Lead_PoolsItems = new List<SelectListItem>();
            foreach (var item in Lead_Pools)
            {
                if (item.Id == id)
                    Lead_PoolsItems.Add(new SelectListItem()
                    {
                        Text = item.Lead_Name,
                        Value = item.Id.ToString(),
                        Selected = true
                    });
                else
                    Lead_PoolsItems.Add(new SelectListItem()
                    {
                        Text = item.Lead_Name,
                        Value = item.Id.ToString()
                    });

            }
            return Lead_PoolsItems;

        }
        public List<SelectListItem> BindTarget(int id)
        {
            var Lead_Pools = db.TargetPeriods.ToList();
            List<SelectListItem> Lead_PoolsItems = new List<SelectListItem>();
            foreach (var item in Lead_Pools)
            {
                if (item.Id == id)
                    Lead_PoolsItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = true
                    });
                else
                    Lead_PoolsItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });

            }
            return Lead_PoolsItems;

        }

    }
    public class CityList
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
    public class PropertyTypeT
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
    public class SelectBinding
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}