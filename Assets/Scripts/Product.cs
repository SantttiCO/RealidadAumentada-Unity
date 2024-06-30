using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product
{
    // Start is called before the first frame update
    private int code;
    private string name;
    private string type;
    private string description;
    private List<string> tallas;

    public Product(int code, string name, string type, string description, List<string> tallas)
    {
        this.code = code;
        this.name = name;
        this.type = type;
        this.description = description;
        this.tallas = tallas;
    }

    public int GetCode()
    {
        return code;
    }

    public string GetName()
    {
        return name;
    }

    public string GetType()
    {
        return type;
    }

    public string GetDescription()
    {
        return description;
    }

    public List<string> GetTallas()
    {
        return tallas;
    }

    public override bool Equals(object obj)
    {
        return obj is Product product &&
               code == product.code &&
               name == product.name &&
               type == product.type &&
               description == product.description &&
               EqualityComparer<List<string>>.Default.Equals(tallas, product.tallas);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(code, name, type, description, tallas);
    }
}
