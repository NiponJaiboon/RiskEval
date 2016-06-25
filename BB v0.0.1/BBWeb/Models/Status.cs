using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class Status
    {
        public int Id { get; set; }
        public int No { get; set; }

        [Required(ErrorMessage = "กรุณากรอกรหัสกระทรวง")]
        [StringLength(25, ErrorMessage = "ความยาวต้องไม่เกิน 25 ตัวอักษร")]
        public string Code { get; set; }

        [Required(ErrorMessage = "กรุณากรอกชื่อกระทรวง")]
        [StringLength(450, ErrorMessage = "ความยาวต้องไม่เกิน 450 ตัวอักษร")]
        public string Name { get; set; }
    }
}