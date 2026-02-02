# Принципи програмування

## Зміст
- [Принципи SOLID](#принципи-solid)
- [Інші принципи програмування](#інші-принципи-програмування)
- [Патерни та практики](#патерни-та-практики)

---

## Принципи SOLID

### Single Responsibility Principle (SRP)
**Принцип єдиної відповідальності**: кожен клас повинен мати лише одну причину для зміни.

#### Дотримання SRP:

**1. Клас `ExtendedDictionaryElement`** ([lab3/ExtendedDictionaryElement.cs#L3-L15](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionaryElement.cs#L3-L15))
```csharp
public class ExtendedDictionaryElement<T, U, V>
{
    public T Key { get; }
    public U Value1 { get; }
    public V Value2 { get; }
}
```
Цей клас має єдину відповідальність - зберігати дані елемента словника (ключ та два значення). Він не відповідає за логіку додавання, видалення чи пошуку.

**2. Клас `ExtendedDictionary`** ([lab3/ExtendedDictionary.cs#L5-L58](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionary.cs#L5-L58))
```csharp
public class ExtendedDictionary<T, U, V> : IEnumerable<ExtendedDictionaryElement<T, U, V>>
```
Відповідає лише за управління колекцією елементів (додавання, видалення, пошук).

**3. Клас `Extensions`** ([lab3/Extensions.cs#L6-L48](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/Extensions.cs#L6-L48))
```csharp
public static class Extensions
```
Відповідає виключно за надання методів розширення для стандартних типів.

---

### Open/Closed Principle (OCP)
**Принцип відкритості/закритості**: класи повинні бути відкриті для розширення, але закриті для модифікації.

#### Дотримання OCP:

**1. Використання дженериків** ([lab3/ExtendedDictionary.cs#L5](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionary.cs#L5))
```csharp
public class ExtendedDictionary<T, U, V>
```
Клас використовує параметризацію типів, що дозволяє розширювати функціональність для різних типів даних без зміни коду класу.

**2. Extension methods** ([lab3/Extensions.cs#L9-L14](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/Extensions.cs#L9-L14))
```csharp
public static string ReverseString(this string value)
public static int CountChar(this string value, char symbol)
```
Методи розширення дозволяють додавати нову функціональність до існуючих типів без їх модифікації.

**3. Реалізація інтерфейсу IEnumerable** ([lab3/ExtendedDictionary.cs#L5](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionary.cs#L5))
```csharp
public class ExtendedDictionary<T, U, V> : IEnumerable<ExtendedDictionaryElement<T, U, V>>
```
Використання інтерфейсу дозволяє розширювати поведінку через додавання нових реалізацій без зміни існуючого коду.

---

### Liskov Substitution Principle (LSP)
**Принцип підстановки Лісков**: об'єкти підкласів повинні коректно замінювати об'єкти базових класів.

#### Дотримання LSP:

**1. Коректна реалізація IEnumerable** ([lab3/ExtendedDictionary.cs#L52-L58](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionary.cs#L52-L58))
```csharp
public IEnumerator<ExtendedDictionaryElement<T, U, V>> GetEnumerator()
{
    return _items.GetEnumerator();
}

IEnumerator IEnumerable.GetEnumerator()
{
    return GetEnumerator();
}
```
`ExtendedDictionary` може бути використаний всюди, де очікується `IEnumerable`, зберігаючи коректну поведінку.

**2. Обмеження через generic constraints** ([lab3/Extensions.cs#L25-L26](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/Extensions.cs#L25-L26))
```csharp
public static int CountOf<T>(this T[] array, T value)
    where T : IEquatable<T>
```
Використання constraint `where T : IEquatable<T>` гарантує, що тип T підтримує порівняння, що забезпечує LSP.

---

### Interface Segregation Principle (ISP)
**Принцип розділення інтерфейсів**: клієнти не повинні залежати від інтерфейсів, які вони не використовують.

#### Дотримання ISP:

**1. Використання мінімального інтерфейсу** ([lab3/ExtendedDictionary.cs#L5](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionary.cs#L5))
```csharp
public class ExtendedDictionary<T, U, V> : IEnumerable<ExtendedDictionaryElement<T, U, V>>
```
Клас реалізує лише `IEnumerable`, а не більш складні інтерфейси як `ICollection` або `IList`, уникаючи зайвих методів.

**2. Специфічні методи розширення** ([lab3/Extensions.cs](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/Extensions.cs))
```csharp
public static string ReverseString(this string value)  // тільки для string
public static int CountOf<T>(this T[] array, T value)  // тільки для масивів
```
Кожен метод розширення додає лише необхідну функціональність для конкретного типу.

---

### Dependency Inversion Principle (DIP)
**Принцип інверсії залежностей**: модулі високого рівня не повинні залежати від модулів низького рівня.

#### Дотримання DIP:

**1. Залежність від абстракції IEnumerable** ([lab3/ExtendedDictionary.cs#L5](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionary.cs#L5))
```csharp
public class ExtendedDictionary<T, U, V> : IEnumerable<ExtendedDictionaryElement<T, U, V>>
```
Клас залежить від абстракції `IEnumerable`, а не від конкретної реалізації колекції.

**2. Використання Generic Constraints** ([lab3/Extensions.cs#L25-L26](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/Extensions.cs#L25-L26))
```csharp
public static int CountOf<T>(this T[] array, T value)
    where T : IEquatable<T>
```
Метод залежить від абстракції `IEquatable<T>`, а не від конкретних типів.

---

## Інші принципи програмування

### DRY (Don't Repeat Yourself)
**Не повторюйся**: уникайте дублювання коду.

#### Дотримання DRY:

**1. Універсальний метод для підрахунку** ([lab3/Extensions.cs#L25-L33](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/Extensions.cs#L25-L33))
```csharp
public static int CountOf<T>(this T[] array, T value)
    where T : IEquatable<T>
{
    int count = 0;
    foreach (var item in array)
        if (item.Equals(value))
            count++;
    return count;
}
```
Замість створення окремих методів для підрахунку різних типів, використовується один generic метод.

**2. Повторне використання GetEnumerator** ([lab3/ExtendedDictionary.cs#L52-L58](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionary.cs#L52-L58))
```csharp
public IEnumerator<ExtendedDictionaryElement<T, U, V>> GetEnumerator()
{
    return _items.GetEnumerator();
}

IEnumerator IEnumerable.GetEnumerator()
{
    return GetEnumerator();  // Повторне використання типізованого методу
}
```

---

### KISS (Keep It Simple, Stupid)
**Тримай це простим**: код повинен бути максимально простим і зрозумілім.

#### Дотримання KISS:

**1. Простий клас даних** ([lab3/ExtendedDictionaryElement.cs#L3-L15](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionaryElement.cs#L3-L15))
```csharp
public class ExtendedDictionaryElement<T, U, V>
{
    public T Key { get; }
    public U Value1 { get; }
    public V Value2 { get; }

    public ExtendedDictionaryElement(T key, U value1, V value2)
    {
        Key = key;
        Value1 = value1;
        Value2 = value2;
    }
}
```
Простий immutable клас без зайвої логіки.

**2. Прості методи розширення** ([lab3/Extensions.cs#L9-L14](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/Extensions.cs#L9-L14))
```csharp
public static string ReverseString(this string value)
{
    char[] chars = value.ToCharArray();
    Array.Reverse(chars);
    return new string(chars);
}
```
Кожен метод виконує одну просту операцію.

---

### YAGNI (You Aren't Gonna Need It)
**Вам це не знадобиться**: не додавайте функціональність, яка не потрібна зараз.

#### Дотримання YAGNI:

**1. Мінімальний набір методів у ExtendedDictionary** ([lab3/ExtendedDictionary.cs](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionary.cs))
```csharp
public void Add(T key, U value1, V value2)
public bool Remove(T key)
public bool ContainsKey(T key)
public bool ContainsValue1(U value)
public bool ContainsValue2(V value)
```
Клас містить лише необхідні методи без зайвої функціональності (наприклад, без методів сортування, фільтрації тощо).

**2. Базові extension methods** ([lab3/Extensions.cs](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/Extensions.cs))
Реалізовано лише потрібні методи розширення: `ReverseString`, `CountChar`, `CountOf`, `Unique`.

---

### Immutability
**Незмінність**: об'єкти не повинні змінюватися після створення.

#### Дотримання Immutability:

**1. Незмінний елемент словника** ([lab3/ExtendedDictionaryElement.cs#L5-L7](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionaryElement.cs#L5-L7))
```csharp
public T Key { get; }
public U Value1 { get; }
public V Value2 { get; }
```
Всі властивості мають лише getter, що робить об'єкт незмінним після створення.

**2. Конструктор ініціалізує всі поля** ([lab3/ExtendedDictionaryElement.cs#L9-L14](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionaryElement.cs#L9-L14))
```csharp
public ExtendedDictionaryElement(T key, U value1, V value2)
{
    Key = key;
    Value1 = value1;
    Value2 = value2;
}
```

---

### Encapsulation
**Інкапсуляція**: приховування внутрішньої реалізації.

#### Дотримання Encapsulation:

**1. Приватне поле _items** ([lab3/ExtendedDictionary.cs#L7](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionary.cs#L7))
```csharp
private readonly List<ExtendedDictionaryElement<T, U, V>> _items = new();
```
Внутрішня колекція прихована від зовнішнього доступу.

**2. Публічні методи для роботи з колекцією** ([lab3/ExtendedDictionary.cs#L11-L40](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionary.cs#L11-L40))
```csharp
public void Add(T key, U value1, V value2)
public bool Remove(T key)
public bool ContainsKey(T key)
```
Доступ до внутрішньої колекції можливий лише через контрольовані методи.

**3. Readonly поле** ([lab3/ExtendedDictionary.cs#L7](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionary.cs#L7))
```csharp
private readonly List<ExtendedDictionaryElement<T, U, V>> _items = new();
```
Використання `readonly` запобігає заміні всієї колекції.

---

## Патерни та практики

### Extension Method Pattern
**Патерн методів розширення**: додавання функціональності до існуючих типів.

#### Реалізація:

**Статичний клас Extensions** ([lab3/Extensions.cs#L6](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/Extensions.cs#L6))
```csharp
public static class Extensions
{
    public static string ReverseString(this string value)
    public static int CountChar(this string value, char symbol)
    public static int CountOf<T>(this T[] array, T value)
    public static T[] Unique<T>(this T[] array)
}
```

#### Використання в Program.cs:

**Приклад 1**: Розширення для string ([lab3/Program.cs#L6-L7](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/Program.cs#L6-L7))
```csharp
Console.WriteLine(text.ReverseString());
Console.WriteLine(text.CountChar('a'));
```

**Приклад 2**: Розширення для масивів ([lab3/Program.cs#L11-L12](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/Program.cs#L11-L12))
```csharp
Console.WriteLine(numbers.CountOf(3));
Console.WriteLine(string.Join(", ", numbers.Unique()));
```

---

### Generic Programming
**Узагальнене програмування**: написання коду, який працює з різними типами.

#### Реалізація:

**1. Generic клас ExtendedDictionary** ([lab3/ExtendedDictionary.cs#L5](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionary.cs#L5))
```csharp
public class ExtendedDictionary<T, U, V>
```
Три параметри типу дозволяють використовувати словник з будь-якими комбінаціями типів.

**2. Generic методи з обмеженнями** ([lab3/Extensions.cs#L25-L26](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/Extensions.cs#L25-L26))
```csharp
public static int CountOf<T>(this T[] array, T value)
    where T : IEquatable<T>
```

**Використання з різними типами** ([lab3/Program.cs#L9-L14](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/Program.cs#L9-L14)):
```csharp
int[] numbers = { 1, 2, 2, 3, 3, 3 };
Console.WriteLine(numbers.CountOf(3));

string[] words = { "hi", "hi", "hello" };
Console.WriteLine(string.Join(", ", words.Unique()));
```

---

### Fail Fast Principle
**Принцип швидкої відмови**: перевіряти помилки якомога раніше.

#### Дотримання:

**Перевірка дублікатів ключів** ([lab3/ExtendedDictionary.cs#L13-L14](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionary.cs#L13-L14))
```csharp
if (ContainsKey(key))
    throw new ArgumentException("Key already exists");
```

**Перевірка існування ключа в індексаторі** ([lab3/ExtendedDictionary.cs#L46-L47](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionary.cs#L46-L47))
```csharp
if (item == null)
    throw new KeyNotFoundException();
```

---

### Defensive Programming
**Захисне програмування**: перевірка вхідних даних та обробка помилок.

#### Реалізація:

**1. Null-безпека** ([lab3/ExtendedDictionary.cs#L21](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/ExtendedDictionary.cs#L21))
```csharp
var item = _items.Find(x => x.Key!.Equals(key));
return item != null && _items.Remove(item);
```

**2. Використання Nullable enable** ([lab3/lab3.csproj#L6](https://github.com/coutAndrii16/kpzLab1/blob/master/lab3/lab3.csproj#L6))
```xml
<Nullable>enable</Nullable>
```
Увімкнення nullable reference types допомагає виявляти потенційні null-помилки на етапі компіляції.

---

## Висновок

Проект демонструє дотримання ключових принципів програмування:
- ✅ Всі принципи SOLID
- ✅ DRY, KISS, YAGNI
- ✅ Immutability та Encapsulation
- ✅ Generic Programming
- ✅ Extension Methods Pattern
- ✅ Fail Fast та Defensive Programming

Код є чистим, підтримуваним та розширюваним завдяки дотриманню цих принципів.