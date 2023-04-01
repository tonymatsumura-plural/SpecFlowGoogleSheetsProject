using NUnit.Framework;
using TechTalk.SpecFlow.CommonModels;

namespace SpecFlowProject2.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        static string _firstNumber;
        static string _secondNumber;
        static string _result;
        static GetSheet ss = new GetSheet();

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        [Given("the first number is (.*)")]
        public static async Task GivenTheFirstNumberIs(string firstNumber)
        {   
            _firstNumber = await ss.GetMethod(firstNumber);
        }

        [Given("the second number is (.*)")]
        public static async Task GivenTheSecondNumberIs(string secondNumber)
        {
            _secondNumber = await ss.GetMethod(secondNumber);
        }

        [When(@"the two numbers are added resulting in (.*)")]
        public async void WhenTheTwoNumbersAreAddedResultingIn(string result)
        {
            _result = await ss.GetMethod(result);
        }

        [Then("the result should be (.*)")]
        public static async Task ThenTheResultShouldBe(string result)
        {
            var resutladoFinal = await ss.GetMethod(result);                       
           
            Assert.AreEqual(_result, resutladoFinal);
        }
    }

    public class GetSheet
    {
        public async Task<string> GetMethod(string namedRange)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://script.google.com/macros/s/AKfycbxum8Y4XjYUgUD8uFN1l202b3LcPmYXTKwsWIQoJSKD7SMKPE2X87TUNkMN8i377xp_/exec?namedRange=" + namedRange);
            var data = await response.Content.ReadAsStringAsync();

            return data;
        }
    }
}