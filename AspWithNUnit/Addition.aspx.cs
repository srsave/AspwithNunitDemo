using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspWithNUnit
{
    public partial class Addition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddClass add = new AddClass();
            lblAdd.Text = lblError.Text = "";
            string valMsg = "";
            string valAdd = "";
            bool flag = add.addition(txtnum1.Text, txtNum2.Text, out valMsg, out valAdd);

            if (valMsg == "")
            { lblAdd.Text = valAdd; }
            else
            { lblError.Text = valMsg; }



        }


    }
    public class AddClass
        {
        public bool addition(string num1, string num2, out string outputText, out string outputAddition)
        {
            bool outputFlag = false;
            string val = "";
            outputText = "";
            outputAddition = "";

            val = validate(num1, num2);
            if (val == "")
            {
                decimal n1, n2, nTotal;
                n1 = Convert.ToDecimal(num1);
                n2 = Convert.ToDecimal(num2);
                nTotal = n1 + n2;
                outputAddition = nTotal.ToString();
                outputFlag = true;

            }
            else
            {
                outputText = val.ToString();
            }

            return outputFlag;
        }
        public string validate(string num1, string num2)
        {
            string err;
            try
            {
                if (num1 == "")
                {
                    err = "PLease enter Num1";
                    return err;
                }
                if (num2 == "")
                {
                    err = "PLease enter Num2";
                    return err;
                }


                decimal num = 0.0M;
                bool numbool = decimal.TryParse(num1, out num);
                if (numbool == false)
                {
                    err = "Num1 is not valid number";
                    return err;
                }
                numbool = decimal.TryParse(num2, out num);
                if (numbool == false)
                {
                    err = "Num2 is not valid number";
                    return err;
                }

                err = "";

            }
            catch
            {
                err = "Technical error.";
            }

            return err;
        }
    }

}