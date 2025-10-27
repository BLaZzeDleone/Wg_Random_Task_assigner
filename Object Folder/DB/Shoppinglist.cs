using System.ComponentModel.DataAnnotations;

public class Shoppinglist{

    [Key]
    public int id {get; set;}

    public string? name {get; set;}

    public int? quantity {get; set;} =1;
    
}