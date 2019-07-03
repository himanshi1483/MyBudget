using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudget.Models
{
    public class SMSData
    {
        [Key]
        public int Id { get; set; }
        public string SMSText { get; set; }
        public DateTime? Date { get; set; }
        public string Label { get; set; }
    }

    public class Labels
    {
        public int LabelId { get; set; }
        public string LabelName { get; set; }
    }

    public class EmailModel
    {
        [LoadColumn(0)]
        public string ID { get; set; }
        [LoadColumn(1)]
        public string SmsText { get; set; }
        [LoadColumn(2)]
        public string Date { get; set; }
        [LoadColumn(3)]
        public string Label { get; set; }
    }

    public class EmailPrediction
    {
        [ColumnName("PredictedLabel")]
        public string SmsText;
    }
}