//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace QA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Patient
    {
        public string ID { get; set; }
        public string p_account { get; set; }
        public string p_Name { get; set; }
        public string Idcard { get; set; }
        public string p_sex { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> Enroll_date { get; set; }
        public string p_tel { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
    }
}
