form test {
	"What is the meaning of life?"
	whatIsMeaning: decimal

	if (whatIsMeaning) {
		"Did you sell a house in 2010?"
		hasSoldHouse: boolean

		if (whatIsMeaning) {
			"Which day is today?"
			dayToday: date
		}
	}
}