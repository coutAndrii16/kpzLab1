using Lab3;

// EXTENSION METHODS

string text = "abracadabra";

Console.WriteLine(text.ReverseString());
Console.WriteLine(text.CountChar('a'));

int[] numbers = { 1, 2, 2, 3, 3, 3 };

Console.WriteLine(numbers.CountOf(3));
Console.WriteLine(string.Join(", ", numbers.Unique()));

string[] words = { "hi", "hi", "hello" };
Console.WriteLine(string.Join(", ", words.Unique()));

// EXTENDED DICTIONARY

var dict = new ExtendedDictionary<int, string, double>();

dict.Add(1, "Apple", 10.5);
dict.Add(2, "Banana", 7.3);

Console.WriteLine(dict[1].Value1);
Console.WriteLine($"Count: {dict.Count}");

foreach (var item in dict)
{
    Console.WriteLine($"{item.Key}: {item.Value1}, {item.Value2}");
}