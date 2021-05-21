using System;

static void SeparateOutput() => Console.WriteLine(new string('-', 90));

static double GetMonthlyRate(double yearlyRate) => yearlyRate / 12 / 100;

static double GetAnnuityPayment(double remainingDebt, int term, double yearlyRate)
{
	double	monthlyRate = GetMonthlyRate(yearlyRate);
	double monthlyRatePowByTerm = Math.Pow(1.0 + monthlyRate, term);
	return (remainingDebt * monthlyRate * monthlyRatePowByTerm) / (monthlyRatePowByTerm - 1.0);
}

static int GetMonthsRecount(double payment, double yearlyRate, double remainingDebt)
{
	double	monthlyRate = GetMonthlyRate(yearlyRate);
	double quotent = payment - monthlyRate * remainingDebt;
	return (int)Math.Round(Math.Log(payment / quotent, 1.0 + monthlyRate));
}

static double GetPercents(double debtSum, double yearlyRate, DateTime month) =>
	debtSum * yearlyRate * DateTime.DaysInMonth(month.Year, month.Month) / 100 /
	(DateTime.IsLeapYear(month.Year) ? 366 : 365);

static double GetOverpayAfterAddPayment(double remainingDebt, double annuityPayment, double rate,
	double percents, int term, DateTime month, bool recountPercent = false)
{
	var overpay = 0.0;
	double debtPaymentPart;

	if (recountPercent)	// we need to recount percent after annuityPayment in this month with specific output
	{
		debtPaymentPart = annuityPayment - percents;
		remainingDebt -= debtPaymentPart;
		month = month.AddMonths(1);
		double nextAnnuityPayment = GetAnnuityPayment(remainingDebt, term - 1, rate);
		double nextPercent = GetPercents(remainingDebt, rate, month);
		Console.WriteLine("| {0:dd.MM.yyyy} | {1,16:N2} | {2,16:N2} | {3,16:N2} | {4,16:N2} |",
			month, nextAnnuityPayment, debtPaymentPart, percents, remainingDebt);
		overpay += annuityPayment;
		annuityPayment = nextAnnuityPayment;
		percents = nextPercent;
		--term;
	}

	while (--term >= 0)
	{
		month = month.AddMonths(1);
		overpay += annuityPayment;
		debtPaymentPart = annuityPayment - percents;
		remainingDebt -= debtPaymentPart;

		if (term == 0 && remainingDebt > 0.0)
		{
			overpay += remainingDebt;
			debtPaymentPart += remainingDebt;
			annuityPayment += remainingDebt;
			remainingDebt = 0;
		}

		if (remainingDebt < 0)
			remainingDebt = 0.0;

		Console.WriteLine("| {0:dd.MM.yyyy} | {1,16:N2} | {2,16:N2} | {3,16:N2} | {4,16:N2} |",
			month, annuityPayment, debtPaymentPart, percents, remainingDebt);
		percents = GetPercents(remainingDebt, rate, month);
	}

	return overpay;
}

// ----------------------------------------------------------------------------

double sum, rate, payment;
int term, selectedMonth;

// entry point: checking and validating arguments
try
{
	if (args.Length < 5)
		throw new Exception("Input error. Check input data and retry.");

	sum = double.Parse(args[0]);
	rate = double.Parse(args[1]);
	term = int.Parse(args[2]);
	selectedMonth = int.Parse(args[3]);
	payment = double.Parse(args[4]);

	// validating input
	if (rate < 0.0 || sum < 0.0 || term < 0 || payment < 0.0 ||
	    selectedMonth < 0 || selectedMonth >= term)
		throw new Exception("Input error. Check input data and retry.");
}
catch (Exception e)
{
	Console.WriteLine(e.Message);
	return;
}

// counting formulas
double annuityPayment = GetAnnuityPayment(sum, term, rate);
double overpay = -sum;
double remainingSum = sum;
var month = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
double percents;

// table header
Console.WriteLine("| {0,10} | {1,16} | {2,16} | {3,16} | {4,16} |",
	"Date", "Payment", "Debt part", "Percents", "Remaining Debt");
SeparateOutput();
Console.WriteLine("| {0:dd.MM.yyyy} | {1,16:N2} | {2,16:N2} | {3,16:N2} | {4,16:N2} |",
	month, annuityPayment, sum, annuityPayment * term - sum, remainingSum);
SeparateOutput();

// first percents
percents = GetPercents(remainingSum, rate, month);
for (var i = 1; i < selectedMonth; i++)
{
	overpay += annuityPayment;
	month = month.AddMonths(1);
	double debtPaymentPart = annuityPayment - percents;
	remainingSum -= debtPaymentPart;

	if (remainingSum < 0)
		remainingSum = 0;

	Console.WriteLine("| {0:dd.MM.yyyy} | {1,16:N2} | {2,16:N2} | {3,16:N2} | {4,16:N2} |",
		month, annuityPayment, debtPaymentPart, percents, remainingSum);
	percents = GetPercents(remainingSum, rate, month);
	--term;
}

// test related default overpay after selected month
double overpayDefault = GetOverpayAfterAddPayment(remainingSum,
	annuityPayment, rate, percents, term, month) + overpay;
SeparateOutput();

// additional payment
overpay += payment;
remainingSum -= payment;

// percent/payment recount
double overpayAfterPercentDecrease = GetOverpayAfterAddPayment(remainingSum,
	annuityPayment, rate, percents, term, month, true) + overpay;
SeparateOutput();

// months recount
term = GetMonthsRecount(annuityPayment, rate, remainingSum);
double overpayAfterTermDecrease = GetOverpayAfterAddPayment(remainingSum,
	annuityPayment, rate, percents, term, month) + overpay;
SeparateOutput();

// output
// Console.WriteLine("Overpay without any decrease:\t{0:N2} rubles", overpayDefault);
Console.WriteLine("Overpay on payment decrease:\t{0:N2} rubles", overpayAfterPercentDecrease);
Console.WriteLine("Overpay on term decrease:\t{0:N2} rubles", overpayAfterTermDecrease);

double diff = Math.Abs(overpayAfterPercentDecrease - overpayAfterTermDecrease);
if (diff < 1.0)
	Console.WriteLine("Overpay is the same.");
else if (overpayAfterPercentDecrease < overpayAfterTermDecrease)
	Console.WriteLine("Percent decrease is better than term decrease by {0:N2} rubles", diff);
else
	Console.WriteLine("Term decrease is better than percent decrease by {0:N2} rubles", diff);
