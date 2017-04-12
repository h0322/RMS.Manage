using HH.RMS.IService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.RMS.Manage.Service.Interface
{
    public interface IAccountManageService
    {
        GridModel QueryAccountToGridByRole(PagerModel pager);
    }
}
