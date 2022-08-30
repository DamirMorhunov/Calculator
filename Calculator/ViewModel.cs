using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{
    internal class ViewModel : ViewModelBase
    {
        #region PrivateFields

        private string _operand ="0";
        private string _operation = "";
        private RelayCommand _clearAll;
        private RelayCommand _operationInput;
        private RelayCommand _operandInput;
        private RelayCommand _pointInput;
        private RelayCommand _equal;
        private RelayCommand _clear;
        private RelayCommand _delete;
        private bool _isCalculationDone = false;
        private bool _hasError = false;
        private bool _isCalculationRepeat = false;
        private Calculation _calculate = new Calculation();
        
        #endregion
        #region Properties

        public string Operand 
        {
            get { return _operand; }
            set 
            { 
                _operand = value;
                OnPropertyChanged(nameof(Operand));
            }

        }
        public string Operation
        {
            get { return _operation; }
            set
            {
                _operation = value;
                OnPropertyChanged(nameof(Operation));
            }
        }
        public bool HasError
        {
            get { return _hasError; }
            set
            {
                if (value == _hasError) return;
                _hasError = value;
                OnPropertyChanged(nameof(HasError));            
            }
        }
        #endregion
        #region Command 
        public ICommand OperationInputCommand => _operationInput;
        public ICommand OperandInputCommand => _operandInput;
        public ICommand PointInputCommand => _pointInput;
        public ICommand ClearAllCommand => _clearAll;
        public ICommand DeleteCommand => _delete;
        public ICommand EqualCommand => _equal;
        public ICommand ClearCommand => _clear;
        #endregion
        #region PrivateMethods
        private void OperandInput(object o)
        {
            if (HasError || _isCalculationDone)
                ClearAll(o);
            if (Operand != "0")
            {
                Operand += o.ToString();
            }
            else Operand = o.ToString();
        }
        private void OperationInput(object o)
        {

            if (HasError) ClearAll(o);
            char @operator = o.ToString()[0];
            _isCalculationDone = false;
            if(!_isCalculationRepeat||Operand is not null)
            {
                if(_isCalculationRepeat)
                {
                    Equal(o);
                    Operation = _calculate.Operand1.ToString() + @operator;
                    _isCalculationDone = false;
                    
                }
                _calculate.Operand1 = double.Parse(Operand,Calculation._numberFormatInfo);
                Operand = null;
                _isCalculationRepeat = true;
            }
            Operation = _calculate.Operand1.ToString() + @operator;
            _calculate.Operator = @operator;
        }
        private void ClearAll(object o)
        {
            Clear(o);
            Operation = null;
            _isCalculationDone = HasError = false;

        }
        private void Clear(object o)
        {
            Operand = "0";
        }
        private void PointInput(object o)
        {
            if (HasError || _isCalculationDone)
                ClearAll(o);
            else if (Operand is null) Operand = "0";
            else if (Operand.Contains(Calculation._numberFormatInfo.NumberDecimalSeparator)) return;
            Operand += Calculation._numberFormatInfo.NumberDecimalSeparator;
        }
        private void Delete(object o)
        {
            if(HasError || _isCalculationDone)
                ClearAll(o);
            if(Operand?.Length > 1)
                Operand = Operand.Remove(Operand.Length - 1);
            else Operand = "0";
          
        }
        private void Equal(object o)
        {
            if (Operation is null && !_isCalculationDone) return;
            if (Operand is not null)
            {
                double operand = double.Parse(Operand, Calculation._numberFormatInfo);
                if (!_isCalculationDone) _calculate.Operand2 = operand;
                else _calculate.Operand1 = operand;
            }
            else _calculate.Operand2 = null;
            double? result = _calculate.Calculate();
            if (result is null)
            {
                HasError = true;
                
            }           
            else Operand = result?.ToString(Calculation._numberFormatInfo);
            _isCalculationDone = true;
            _isCalculationRepeat = false;
            Operation = null;
        }
        #endregion
        public ViewModel()
        {
            _delete = new RelayCommand(Delete);
            _pointInput = new RelayCommand(PointInput);
            _operationInput = new RelayCommand(OperationInput);
            _operandInput = new RelayCommand(OperandInput);
            _clearAll = new RelayCommand(ClearAll);
            _clear = new RelayCommand(Clear);
            _equal = new RelayCommand(Equal);
        }
    }
}
