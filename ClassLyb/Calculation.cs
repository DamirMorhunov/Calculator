namespace ClassLyb
{
    public class Calculation
    {
        
        
            public double? Operand1 { get; set; }
            public double? Operand2 { get; set; }
            public char? Operator { get; set; }
            public double? Calculate()
            {
                if (Operand1.HasValue) Operand2 = Operand2 ?? Operand1;
                switch (Operator)
                {
                    case '+':
                        return Operand1 + Operand2;
                    case '-':
                        return Operand1 - Operand2;
                    case '*':
                        return Operand1 * Operand2;
                    case '/':
                        return (Operand2 == 0) ? null : Operand1 / Operand2;
                    default: return null;
                }
            }
            public void Reset() => Operand1 = Operand2 = Operator = null;


        
    }
}