
namespace QA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Consult
    {
        public string Id { get; set; }

        //创建时间
        public System.DateTime create_date { get; set; }

        //咨询内容
        public string Q_describe { get; set; }

        //咨询状态
        public Nullable<int> state { get; set; }

        //解答内容
        public string A_describe { get; set; }

        //患者编号
        public string p_id { get; set; }

        //医生编号
        public string d_id { get; set; }

        //评分
        public Nullable<int> points { get; set; }
    }
}
