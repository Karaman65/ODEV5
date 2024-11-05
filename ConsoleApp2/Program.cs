using System;
using System.Collections.Generic;

class Program
{
    // Infix'i Postfix'e dönüştürme fonksiyonu
    static string InfixToPostfix(string infix)
    {
        Stack<char> yığın = new Stack<char>();  // Operatörleri yığında saklamak için
        string postfix = "";  // Sonuç postfix ifadesi

        foreach (char c in infix)
        {
            if (Char.IsDigit(c))  // Eğer karakter bir sayı ise, direkt olarak postfix'e ekle
                postfix += c;
            else if (c == '(')  // Sol parantez
                yığın.Push(c);  // Sol parantezi yığına ekle
            else if (c == ')')  // Sağ parantez
            {
                // Sol parantez gelene kadar yığındaki operatörleri postfix'e ekle
                while (yığın.Peek() != '(')
                    postfix += yığın.Pop();
                yığın.Pop();  // Parantezleri yığından çıkar
            }
            else  // Eğer karakter bir operatörse
            {
                // Yığındaki operatörler ile şu anki operatörün önceliğini karşılaştır
                while (yığın.Count > 0 && yığın.Peek() != '(')
                    postfix += yığın.Pop();  // Yığındaki operatörleri postfix'e ekle
                yığın.Push(c);  // Şu anki operatörü yığına ekle
            }
        }

        // Yığındaki kalan operatörleri postfix ifadesine ekle
        while (yığın.Count > 0)
            postfix += yığın.Pop();

        return postfix;
    }

    // Postfix hesaplama fonksiyonu
    static int PostfixHesapla(string postfix)
    {
        Stack<int> yığın = new Stack<int>();  // Sayıları yığının içinde tut

        foreach (char c in postfix)
        {
            if (Char.IsDigit(c))  // Eğer karakter bir sayıysa
                yığın.Push(c - '0');  // Sayıyı yığına ekle
            else  // Eğer karakter bir operatörse
            {
                int b = yığın.Pop();  // Yığından ikinci sayıyı al
                int a = yığın.Pop();  // Yığından birinci sayıyı al
                switch (c)
                {
                    case '+': yığın.Push(a + b); break;  // Toplama işlemi
                    case '-': yığın.Push(a - b); break;  // Çıkarma işlemi
                    case '*': yığın.Push(a * b); break;  // Çarpma işlemi
                    case '/': yığın.Push(a / b); break;  // Bölme işlemi
                }
            }
        }

        return yığın.Pop();  // Sonuç olarak yığındaki son sayıyı döndür
    }

    static void Main()
    {
        // Kullanıcıdan infix (standart) ifade alınır
        Console.Write("Infix ifadeyi girin: ");
        string infix = Console.ReadLine();

        // Infix ifadesi postfix formatına dönüştürülür
        string postfix = InfixToPostfix(infix);
        Console.WriteLine("Postfix: " + postfix);

        // Postfix ifadesi hesaplanır ve sonuç yazdırılır
        int sonuç = PostfixHesapla(postfix);
        Console.WriteLine("Sonuç: " + sonuç);
    }
}
