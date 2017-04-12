using HH.RMS.Common.Constant;
using HH.RMS.Entity;
using HH.RMS.IService.Model;
using HH.RMS.Manage.Repository;
using HH.RMS.Manage.Service.Interface;
using HH.RMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.RMS.Manage.Service
{
    public class AccountManageService:IAccountManageService
    {
        private IRepository<AccountEntity> _accountRepository;
        public AccountManageService(IRepository<AccountEntity> accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public GridModel QueryAccountToGridByRole(PagerModel pager)
        {
            if (pager == null)
            {
                pager = new PagerModel();
            }
            int roleOrder = (int)AccountModel.CurrentSession.accountType;
            try
            {
                using (var db = new ManageDbContext())
                {
                    var q = (from a in _accountRepository.Query(db)
                             where (string.IsNullOrEmpty(pager.searchText) || a.accountName.Contains(pager.searchText))
                             && (int)a.accountType >= (int)AccountModel.CurrentSession.accountType
                             && (pager.searchStatus == 0 || a.status == (AccountStatusType)pager.searchStatus)
                             && (pager.searchDateFrom == null || a.createTime > pager.searchDateFrom)
                             && (pager.searchDateTo == null || a.createTime < pager.searchDateTo)
                             && (pager.searchRole == 0 || (a.roleBitMap & pager.searchRole) == pager.searchRole)
                             select new AccountModel()
                             {
                                 id = a.id,
                                 accountName = a.accountName,
                                 email = a.email,
                                 status = a.status,
                                 score = a.score,
                                 amount = a.amount,
                                 createTime = a.createTime,
                                 birthday = a.birthday,
                                 cityId = a.cityId,
                                 countryId = a.countryId,
                                 name = a.name,
                                 nickName = a.nickName,
                                 provinceId = a.provinceId,
                                 sex = a.sex,
                                 levelId = a.id,
                             });
                    IQueryable<AccountModel> qPager = null;
                    if (pager.rows > 0 && pager.page > 0)
                    {
                        qPager = q.OrderByDescending(m => m.id).Take(pager.rows * pager.page).Skip(pager.rows * (pager.page - 1));
                        GridModel list = new GridModel()
                        {
                            rows = qPager.ToList(),
                            total = q.Count()
                        };
                        return list;
                    }
                    return new GridModel() { rows = q.ToList(), total = q.Count() };
                }
            }
            catch (Exception ex)
            {
                Config.log.Error("AccountService.QueryAccountByRole", ex);
                return null;
            }
        }
    }
}
