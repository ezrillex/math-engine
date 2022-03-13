using NoIDEA;

Console.Title = "No IDEA v0.1";
p($"Hello, {Environment.UserName}!");

char[] arithmetic = { '+', '-', 'x', '*', '/' };
char[] numeric = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', ' ' };
char[] valid_inputs = arithmetic.Concat(numeric).ToArray();



p("Basic Arithmetic Solver!");

start:

p("Input some math?");
string input = r();

// ------------------------------------------------------------- PARSE PROCESS

input = input.ToLower();
input = input.Trim();

bool valid = true;
foreach(char c in input)
{
    if (!valid_inputs.Contains(c))
    {
        valid = false;
    }
}
if (string.IsNullOrEmpty(input)) valid = false;


if(valid is false)
{
    p("ERROR, invalid character in formula.");
    goto start;
}

// get operations
List<char> operations = new List<char>();
foreach(char c in input)
{
    if (arithmetic.Contains(c))
    {
        operations.Add(c);
    }
}

// get numbers and convert them
string[] numbers = input.Split(arithmetic);
List<double> nums = new List<double>();
foreach(string number in numbers)
{
    nums.Add(double.Parse( number ));
}

List<Arithmetic> ops = new List<Arithmetic>();
// tokenize operands
foreach(char o in operations)
{
    switch (o)
    {
        case '+':
            ops.Add(Arithmetic.Addition);
            break;
        case '-':
            ops.Add(Arithmetic.Substraction);
            break;
        case 'x':
        case '*':
            ops.Add(Arithmetic.Multiplication);
            break;
        case '/':
            ops.Add(Arithmetic.Division);
            break;
        default:
            throw new Exception("Error: Unknown Symbol " + o);
    }
}


// recombine formula elements
List<element> elements = new List<element>();
bool swap = false;
while(ops.Count > 0 || nums.Count > 0)
{
    if (swap) // add arithmetic
    {
        elements.Add(new element(true, false,op: ops[0]));
        ops.RemoveAt(0);
    }
    else // add number
    {
        elements.Add(new element(false, true, value: nums[0] ));
        nums.RemoveAt(0);
    }
    swap = !swap;
}


// PEMDAS --------------------------------------------------------- MATH PROCESS

void ShowStep(string explanation = "")
{
    w("=");
    foreach(element e in elements)
    {
        w(e.GetAtom());
    }
    if(explanation != "")
    {
        explanation = "# " + explanation;
    }
    p($"\t{explanation}");
}

ShowStep();

// Solve multiplications/divisions
bool solved = false;
while(solved is false)
{
    int current = 0;
    bool foundMD = false;
    foreach(element element in elements)
    {
        if (element.IsOperand)
        {
            if(element.operation == Arithmetic.Multiplication || element.operation == Arithmetic.Division)
            {
                foundMD = true;
                current = elements.IndexOf( element);
                break;
            }
        }
    }

    if (foundMD)
    {
        double result;
        double left = elements[current - 1].Value;
        double right = elements[current +1 ].Value;
        if(elements[current].operation == Arithmetic.Multiplication)
        {
            result = Do.Multiply(left, right);
        }
        else
        {
            result = Do.Divide(left, right);
        }

        elements[current - 1] = new element(false, true, result);
        elements.RemoveAt(current + 1);
        elements.RemoveAt(current);

        ShowStep("Multiply/Divide");
    }
    else
    {
        solved = true;
    }
}

// Solve Addition/Substraction
solved = false;
while (solved is false)
{
    int current = 0;
    bool foundAS = false;
    foreach (element element in elements)
    {
        if (element.IsOperand)
        {
            if (element.operation == Arithmetic.Addition || element.operation == Arithmetic.Substraction)
            {
                foundAS = true;
                current = elements.IndexOf(element);
                break;
            }
        }
    }

    if (foundAS)
    {
        double result;
        double left = elements[current - 1].Value;
        double right = elements[current + 1].Value;
        if (elements[current].operation == Arithmetic.Addition)
        {
            result = Do.Add(left, right);
        }
        else
        {
            result = Do.Substract(left, right);
        }

        elements[current - 1] = new element(false, true, result);
        elements.RemoveAt(current + 1);
        elements.RemoveAt(current);

        ShowStep("Add/Substract");
    }
    else
    {
        solved = true;
    }
}

// answer should be already shown here

goto start;


p("Press any key to Exit");
Console.ReadKey();

static void w(object value)
{
    Console.Write(value);
}

static void p(object value)
{
    Console.WriteLine(value);
}

static string r()
{
    return Console.ReadLine() ?? "" ;
}
