using DVlD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD.ctrPersonCardWithFilter;

namespace DVLD
{
    public partial class ctrPersonCardWithFilter : UserControl
    {
        private clsPerson _Person;

 /*Old Way for Event
         public event Action<int> OnPersonSelected;
                protected virtual void PersonSelected(int PersonID)
                {
                    Action<int> handler = OnPersonSelected;
                    if (handler != null)
                    {
                        handler(PersonID);
                    }
                }*/

        public class PersonSelectionEventArgs : EventArgs
        {
            public clsPerson PersonInfo { get; }

            public PersonSelectionEventArgs(clsPerson PersonInfo)
            {
                this.PersonInfo = PersonInfo;
            }
        }
        public void RaisePersonSelected(clsPerson PersonINfo)
        {
            RaisePersonSelected( new PersonSelectionEventArgs(PersonINfo));
        }
        protected virtual void RaisePersonSelected(PersonSelectionEventArgs e)
        {
           OnPersonSelected?.Invoke(this, e);
        }

        public event EventHandler<PersonSelectionEventArgs> OnPersonSelected;
        
        public ctrPersonCardWithFilter()
        {
            InitializeComponent();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            frmAddNewPerson newUser = new frmAddNewPerson();
            newUser.DataBack += _PeronID;
            _Person = new clsPerson();
            newUser.ShowDialog();
            if (OnPersonSelected != null && cmbFindBy.Enabled)
            {

                RaisePersonSelected(_Person);
            }


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            switch (cmbFindBy.SelectedIndex)
            {
                case 0:
                    _Person = clsPerson.Find(Convert.ToInt32(txtFindBy.Text));
                    if (_Person != null)
                    {

                        ucPersonDetails1.LoadPersonInfo((int)_Person.PersonID);


                    }
                    else
                    {
                        MessageBox.Show($"No person with this PersonID {txtFindBy.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _Person=new clsPerson();
                        
                    }
                    break;
                case 1:
                    _Person = clsPerson.Find(txtFindBy.Text);
                    if (_Person != null)
                    {
                        ucPersonDetails1.LoadPersonInfo((int)_Person.PersonID);


                    }

                    else
                    {
                        MessageBox.Show($"No person with NatoinalNO:  {txtFindBy.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _Person = new clsPerson();
                    }
                    break;
                  


            }
            if (OnPersonSelected != null && cmbFindBy.Enabled)
            {

                RaisePersonSelected(new PersonSelectionEventArgs(_Person));
            }



        }

        private void cmbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ucPersonDetails1_Load(object sender, EventArgs e)
        {
            cmbFindBy.SelectedIndex=0;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
       {
            if (cmbFindBy.SelectedIndex == 1)
                return;
            
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
            
            else
                e.Handled = false; 


        }
        
        private void _PeronID(object sender, int PersonID)
        {
            ucPersonDetails1.LoadPersonInfo(PersonID);
            _Person=clsPerson.Find(PersonID);
        }

        public  UCPersonDetails PersonsCard()
        {
            return ucPersonDetails1;
        }
        
        public void FilterBy( int PersonID)
        {
            cmbFindBy.SelectedIndex = 0;
            txtFindBy.Text = PersonID.ToString();
            groupBox1.Enabled=false;    
        }

    
    
    }
}
