# Verex
Verex is a verbal Regex builder, 
to help you build complex regular expressions 
by using readable C# or VB.NET code.


# Install the Verex NuGet to your project:
Use this command line in Package Manager:
```
PM>  Install-Package Verex -Version 1.2.0
```

And then add these using statments to your code page:
```
 using RegexBuilder;
 using static RegexBuilder.Patterns;
 using static RegexBuilder.Patterns.Symbols;
```

# Sample 1: Email Verex:
```
var ValidFirstChar = EnglishLetterOrHyphen;
var ValidChars = EnglishLetterOrHyphenOrDigit & '.' & '-';

var email = WordEdge
        + ValidFirstChar
        + ValidChars[0, 19]
        + "@"
        + ValidFirstChar
        + ValidChars[2, 14]
        + "."
        + ValidFirstChar
        + ValidChars[1, 9]
        + WordEdge;

textBox1.Text = email.ToString();

var vrx = new Verex(email);
var txt = "My email is msvbnet@hotmail.com .";
var m = vrx.Match(txt);
if (m.Success)
    MessageBox.Show(m.Value);
```
The email Verex will generate:
```
\b[_a-zA-Z][_.\-a-zA-Z0-9]{0,19}@[_a-zA-Z][_.\-a-zA-Z0-9]{2,14}\.[_a-zA-Z][_.\-a-zA-Z0-9]{1,9}\b
```
# Sample 2: URL Verex:
```
var Name = (
              InRange(('A', 'Z'), ('a', 'z'), ('0', '9')) & '-'
            )[1,20];
var url = WordEdge +
                ("http" + "s".Maybe() + "://").NoneOrOnce() +
                "www.".NoneOrOnce() +
                Name + ("." + Name).NoneOrMany() +
                "." + OptionsGroup(Text("com") | "org" | "net").IgnoreCase() +
                Maybe(@"/") +
                WordEdge;

textBox1.Text = url.Expression;

var vrx = new Verex(url, RegexOptions.IgnoreCase);
var txt = "My site is http://mhmdhmdy.blogspot.com/ .";
var m = vrx.Match(txt);
if (m.Success)
    MessageBox.Show(m.Value);
```
The url Verex will generate:
```
\b(?:https?://)?(?:www\.)?[\-A-Za-z0-9]+(?:\.[\-A-Za-z0-9]+)*\.(?i-:com|org|net)/?\b
```

# Sample 3: IP Verex:
```
var validNo = (Maybe("1") + Digit[1, 2]) |
            ("2" +
                (
                    (InRange('0', '4') + Digit) | ("5" + InRange('0', '5'))
                )
            );

var IP = WordEdge + validNo + ("." + validNo)[3] + WordEdge;

textBox1.Text = IP.Expression;
var vrx = new Verex(IP);
var txt = "My IP is 10.0.127.212 .";
var m = vrx.Match(txt);
if (m.Success)
    MessageBox.Show(m.Value);
```
The Ip Verex will generate:
```
\b(?:1?\d{1,2}|2(?:[0-4]\d|5[0-5]))(?:\.(?:1?\d{1,2}|2(?:[0-4]\d|5[0-5]))){3}\b
```

# Example 4: Balanced Brackets:
```
string input = "((x + 2) * (4 + 3)) / 2";
var x = Verex.BalancedContents(input, "(", ")");
if (x == null)
   Console.WriteLine("The ( ) are not balanced");
else
   foreach (Content c in x)
      Console.WriteLine(c.Value);
```

The output will be:
```
(x + 2) * (4 + 3)
x + 2
4 + 3
```