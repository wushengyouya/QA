using QA.App_Start;
using QA.Models;
using QA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QA.Controllers
{
    public class UserController : Controller
    {
        private OnlineQEntities OnlineQEntities = new OnlineQEntities();
        // GET: Login

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string userAccount, string userPwd)
        {
            //病人登录          
            var patientInfo = OnlineQEntities.Patients.FirstOrDefault(p => p.p_account.Equals(userAccount));

            if (patientInfo != null)
            {
                if (patientInfo.p_account.Equals(userAccount) && patientInfo.Password.Equals(userPwd))
                {
                    Session["user"] = patientInfo;
                    //跳转至选择科室界面,目前展示的是个人信息界面
                    return Redirect("/Consult/Index");
                }
                else
                {
                    ViewBag.Msg = "用户名或密码错误";

                }
            }
           

            return View();
        }

        
        public ActionResult DoctorLogin(string userAccount, string userPwd)
        {
            //医生登录
            var doctorInfo = OnlineQEntities.Doctors.FirstOrDefault(d => d.d_account.Equals(userAccount));
            if(doctorInfo != null)
            {
                if (doctorInfo.d_account.Equals(userAccount) && doctorInfo.password.Equals(userPwd))
                {
                    Session["doctor"] = doctorInfo;
                    //跳转至回答咨询界面
                    return Redirect("/User/DoctorPersonal");
                }
                else
                {
                    ViewBag.Msg = "用户名或密码错误";

                }
            }
            
            return View();
        }

        /// <summary>
        /// 医生个人中心
        /// </summary>
        /// <returns></returns>
        [DoctorActionFilter]
        public ActionResult DoctorPersonal(int pageIndex = 1, int pageSize = 2)
        {
            return View(DoctorData(pageIndex, pageSize));
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }



        public ActionResult Register(Patient patient)
        {
            if (patient.p_account != null)
            {
                patient.ID = Guid.NewGuid().ToString("N");
                patient.Enroll_date = DateTime.Now;
                patient.Birthday = DateTime.Now;
                OnlineQEntities.Patients.Add(patient);
                if (SaveChanges() > 0)
                {
                    return Redirect("/Consult/Index");
                }
            }
            ViewBag.Msg = "未知错误";
            return View();
        }

        /// <summary>
        /// 医生个人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DoctorInfo(string id)
        {
            var doctor = OnlineQEntities.Doctors.First(d => d.Id == id);
            return Json(doctor);
        }

        /// <summary>
        /// 修改患者信息
        /// </summary>
        /// <param name="doctor"></param>
        /// <returns></returns>
        public string UpdatePatientInfo(string userId, string newpas)
        {
            OnlineQEntities.Patients.FirstOrDefault(p => p.ID == userId).Password = newpas;
            SaveChanges();
            return "y";
        }



        /// <summary>
        /// 保存修改
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return OnlineQEntities.SaveChanges();
        }


        /// <summary>
        /// 医生所有咨询数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<DoctorCenterViewModel> DoctorData(int pageIndex, int pageSize)
        {
            var currentDoctor =(Doctor) Session["doctor"];
            var tempData = OnlineQEntities.Consults.Where(d => d.d_id.Equals(currentDoctor.Id)).ToList();

            //计算医生平均分
            ViewBag.average = tempData.Sum(d => d.points) / tempData.Count;

            //当前医生所有咨询条数
            var consults = (from c in tempData
                            join p in OnlineQEntities.Patients 
                            on c.p_id equals p.ID
                           select new DoctorCenterViewModel
                           {

                               Patient = p,
                               Consult = c,
                           });
            double totalCount = consults.Count() * 1.0;
            //总页数
            ViewBag.PageCount = Math.Ceiling(totalCount / pageSize);

            //当前页码
            ViewBag.PageIndex = pageIndex;

            //当前页条数
            consults = consults.OrderBy(p => p.Consult.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return consults;
        }
    }
}