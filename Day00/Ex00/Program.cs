using System;

static void SeparateOutput() => Console.WriteLine(new string('-', 90));

static void PrintTableRow(DateTime month, double annuityPay, double debtPay, double percents, double remainingDebt) =>
	Console.WriteLine($"| {month:dd.MM.yyyy} | {annuityPay,16:N2} | {debtPay,16:N2} | {percents,16:N2} | {remainingDebt,16:N2} |");

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
		PrintTableRow(month, nextAnnuityPayment, debtPaymentPart, percents, remainingDebt);
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

		PrintTableRow(month, annuityPayment, debtPaymentPart, percents, remainingDebt);
		percents = GetPercents(remainingDebt, rate, month);
	}

	return overpay;
}

// ----------------------------------------------------------------------------

double sum, rate, payment, annuityPayment;
int term, selectedMonth;

// entry point: checking and validating arguments (TryParse is faster)
if (args.Length < 5 ||
	!(double.TryParse(args[0], out sum) &&
	double.TryParse(args[1], out rate) &&
	int.TryParse(args[2], out term) &&
	int.TryParse(args[3], out selectedMonth) &&
	double.TryParse(args[4], out payment)) ||
	rate < 0.0 || sum < 0.0 || term < 0 || payment < 0.0 ||
	selectedMonth < 0 || selectedMonth >= term)
{
	Console.WriteLine("Input error. Check input data and retry.");
	return;
}

annuityPayment = GetAnnuityPayment(sum, term, rate);
// invalid for percent decrease recount if payment is bigger than debt left
// values below zero in table in this case
if (annuityPayment * (term - selectedMonth) < payment)
{
	Console.WriteLine("Input error. Check input data and retry.");
	return;
}

var month = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
double overpay = -sum;
double remainingSum = sum;
double percents;

// table header
Console.WriteLine("| {0,10} | {1,16} | {2,16} | {3,16} | {4,16} |",
	"Date", "Payment", "Debt part", "Percents", "Remaining Debt");
SeparateOutput();
PrintTableRow(month, annuityPayment, sum, annuityPayment * term - sum, remainingSum);
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

	PrintTableRow(month, annuityPayment, debtPaymentPart, percents, remainingSum);
	percents = GetPercents(remainingSum, rate, month);
	--term;
}

// test related default overpay after selected month
// double overpayDefault = GetOverpayAfterAddPayment(remainingSum,
// 	annuityPayment, rate, percents, term, month) + overpay;
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
Console.WriteLine($"Overpay on payment decrease:\t{overpayAfterPercentDecrease:N2} rubles");
Console.WriteLine($"Overpay on term decrease:\t{overpayAfterTermDecrease:N2} rubles");

double diff = Math.Abs(overpayAfterPercentDecrease - overpayAfterTermDecrease);
if (diff < 1.0)
	Console.WriteLine("Overpay is the same.");
else if (overpayAfterPercentDecrease < overpayAfterTermDecrease)
	Console.WriteLine($"Percent decrease is better than term decrease by {diff:N2} rubles");
else
	Console.WriteLine($"Term decrease is better than percent decrease by {diff:N2} rubles");
