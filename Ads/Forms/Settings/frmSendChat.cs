using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Classes;
using FMessegeBox;

namespace Ads.Forms.Settings
{
    public partial class frmSendChat : Form
    {
        public frmSendChat()
        {
            InitializeComponent();
        }

        private void txtCity_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtCity);
        }

        private void txtCat1_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtCat1);
        }

        private void txtCat3_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtCat3);
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtAddress);
        }

        private void txtCat2_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtCat2);
        }

        private void txtChatCount_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtChatCount);
        }

        private void txtChatCount_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtChatCount);
        }

        private void txtCat2_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtCat2);
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtAddress);
        }

        private void txtCat3_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtCat3);
        }

        private void txtCat1_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtCat1);
        }

        private void txtCity_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtCity);
        }

        private void frmSendChat_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F5:
                        btnFinish.PerformClick();
                        break;
                    case Keys.Escape:
                        btnCancel.PerformClick();
                        break;
                }
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearchAdv_Click(object sender, EventArgs e)
        {
            try
            {
                var ofd = new OpenFileDialog();
                ofd.Filter = @"txt |*.txt";
                ofd.Title = "لطفا فایل متن چت را انتخاب نمایید";
                if (ofd.ShowDialog() == DialogResult.OK)
                    txtAddress.Text = ofd.FileName;
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private async void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCity.Text))
                {
                    FarsiMessegeBox.Show("لطفا شهر مورد نظر را وارد نمایید");
                    txtCity.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtChatCount.Text))
                {
                    FarsiMessegeBox.Show("لطفا تعداد چت ارسالی مورد نظر را وارد نمایید");
                    txtChatCount.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    FarsiMessegeBox.Show("لطفا آدرس فایل متنی مورد نظر را وارد نمایید");
                    txtAddress.Focus();
                    return;
                }

                var list = File.ReadAllLines(txtAddress.Text).ToList();
                var divar = await DivarAdv.GetInstance();
                await divar.SendChat(list, int.Parse(txtChatCount.Text), txtCity.Text, txtCat1.Text, txtCat2.Text,
                    txtCat3.Text);
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void txtChatCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtSetter.KeyPress_Whitout_Dot(txtChatCount, e);
        }
    }
}
