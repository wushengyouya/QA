using QA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QA.Controllers
{
    public class UserController : Controller
    {
        private OnlineQEntities onlineQEntities = new OnlineQEntities();
        // GET: Login

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login(int flag,string userAccount,string userPwd)
        {
            //病人登录
            if (flag==0)
            {
                var patientInfo = onlineQEntities.Patients.FirstOrDefault(p => p.p_account.Equals(userAccount));
                if(patientInfo !=null && patientInfo.p_account.Equals(userAccount) && patientInfo.Equals(userPwd))
                {
                    //跳转至选择科室界面,目前展示的是个人信息界面
                    return View("PatientPersonal");
                }
                   
            }

            //医生登录
            var doctorInfo = onlineQEntities.Doctors.FirstOrDefault(d => d.d_account.Equals(userAccount));
            if(doctorInfo!=null && doctorInfo.d_account.Equals(userAccount) && doctorInfo.password.Equals(userPwd))
            {
                //跳转至回答咨询界面
                return View("DoctorPersonal");
            }
            return Content("密码或用户名错误");
        }

        

        

        /// <summary>
        /// 医生个人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DoctorInfo(string id)
        {
            var doctor = onlineQEntities.Doctors.First(d => d.Id == id);
            return Json(doctor);
        }

        /// <summary>
        /// 修改患者信息
        /// </summary>
        /// <param name="doctor"></param>
        /// <returns></returns>
        public ActionResult UpdatePatientInfo(Doctor doctor)
        {
            onlineQEntities.Entry(doctor);
            SaveChanges();
            return View("PatientPersonal");
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
           return  onlineQEntities.SaveChanges();
        }
    }
}