using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD
{
    public class ScreenLoader
    {
        public delegate void ButtonAction();
        public delegate void LoadDataSet();
        public delegate void FilteringAction();
       
        private ButtonAction _btnAddAction;
        private ButtonAction _btnReleaseAction;
        private LoadDataSet _DataSet;
        private FilteringAction _FilteringAction;
        public ScreenLoader(ButtonAction ButtonAction,LoadDataSet DataSet, FilteringAction filteringAction)
        {
            this._btnAddAction = ButtonAction;
            this._DataSet = DataSet;
            this._FilteringAction= filteringAction;
        }
          public ScreenLoader(ButtonAction ButtonAddAction,LoadDataSet DataSet, FilteringAction filteringAction, ButtonAction ButtonRelaseAction)
          {
            this._btnAddAction = ButtonAddAction;
            this._DataSet = DataSet;
            this._FilteringAction= filteringAction;
            this._btnReleaseAction= ButtonRelaseAction;
          }
        public void btnAddClick()
        {
            _btnAddAction();
        }
        public void DtaSetLoad()
        {
            _DataSet();
        }
        public void Filtering()
        {
            _FilteringAction();
        }
        public void btnRelaseClick()
        {
            _btnReleaseAction();
        }



    }
}
