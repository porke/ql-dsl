form test {
	"What was the selling price?"
      sellingPrice: money
	"What is the market rate?"
      marketRate: int

    "Value house:"
      valueResidue: money = 
        (money * int)
}