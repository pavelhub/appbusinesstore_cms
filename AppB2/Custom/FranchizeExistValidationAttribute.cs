using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppB2.Custom
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FranchizeExistAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int id;
            if (int.TryParse(value.ToString(), out id))
                return DAL.DataManager.IsFrinchizerExist(id);
            else return false;         
        }
    }
}