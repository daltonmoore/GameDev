from colorama import Fore, Back, Style

class Player:
	chips = 0
	total = 0
	card_list = []

	def add_card(self, card):
		self.card_list.append(card)
		if 'ace' not in card.name:
			self.total += card.value
		else:
			# this is an ace in your hand
			temp = self.total + card.value[1]
			if temp > 21:
				self.total += card.value[0] # ace has value of 1
			else:
				self.total += card.value[1] # ace has value of 11

	def reset_cards(self):
		self.card_list = []
		self.total = 0

	def add_chips(self, chips):
		self.chips += chips

	def __init__(self, chips):
		self.chips = chips
		self.card_list = []

	def __str__(self):
		output = ''
		for i, card in enumerate(self.card_list):
			if 'hearts' in card.name:
				output += Fore.RED
			elif 'diamonds' in card.name:
				output += Fore.CYAN
			elif 'clubs' in card.name:
				output += Fore.GREEN
			elif 'spades' in card.name:
				output += Fore.MAGENTA
			output += card.name + Fore.WHITE

			if i < len(self.card_list) -1:
				output += ', '

		return output