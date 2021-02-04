# Muffin Dev for Unity - Generators

## Usage

Example to generate an enumeration:

```cs
public static class MyEnumGenerator
{
    public static void GenerateEnum()
    {
        CSGenerator generator = new CSGenerator();
        CSEnum enum = generator.AddBuilder(new CSEnum("MyEnum"));
        enum.AddItem("Banana", 0);
        enum.AddItem("Lemon", 1);
        enum.AddItem("Strawberry", 2);

        generator.Generate($"{Application.dataPath}/Assets/Enums");
    }

    public static GenerateEnumUsingShortcut()
    {
        CSGenerator generator = new CSGenerator();
        generator.Enum("MyEnum2")
            .AddItem("Banana", 0);
            .AddItem("Lemon", 1);
            .AddItem("Strawberry", 2);
        
        generator.Generate($"{Application.dataPath}/Assets/Enums");
    }
}
```

Example to generate a JSON file:

```cs
public static class MyJSONGenerator
{
    public static void GenerateJSON()
    {
        JSONGenerator generator = new JSONGenerator();
        JSONObject root = generator.AddBuilder(new JSONObject());
        root.Add("username", "Player1");
        root.Add("score", 25465100);
        root.Add("Highscores", new JSONArray(new int[]
        {
            6541565,
            65156,
            654,
            2
        }));

        generator.Generate($"{Application.dataPath}/Assets/JSON");
    }
}
```