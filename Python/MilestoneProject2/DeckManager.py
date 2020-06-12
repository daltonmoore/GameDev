from Card import Card
import random

all_cards = [
('ace of clubs', 'clubs', (1, 11)),
('two of clubs', 'clubs', 2),
('three of clubs', 'clubs', 3),
('four of clubs', 'clubs', 4),
('five of clubs', 'clubs', 5),
('six of clubs', 'clubs', 6),
('seven of clubs', 'clubs', 7),
('eight of clubs', 'clubs', 8),
('nine of clubs', 'clubs', 9),
('ten of clubs', 'clubs', 10),
('jack of clubs', 'clubs', 10),
('queen of clubs', 'clubs', 10),
('king of clubs', 'clubs', 10),
('ace of spades', 'spades', (1, 11)),
('two of spades', 'spades', 2),
('three of spades', 'spades', 3),
('four of spades', 'spades', 4),
('five of spades', 'spades', 5),
('six of spades', 'spades', 6),
('seven of spades', 'spades', 7),
('eight of spades', 'spades', 8),
('nine of spades', 'spades', 9),
('ten of spades', 'spades', 10),
('jack of spades', 'spades', 10),
('queen of spades', 'spades', 10),
('king of spades', 'spades', 10),
('ace of diamonds', 'diamonds', (1, 11)),
('two of diamonds', 'diamonds', 2),
('three of diamonds', 'diamonds', 3),
('four of diamonds', 'diamonds', 4),
('five of diamonds', 'diamonds', 5),
('six of diamonds', 'diamonds', 6),
('seven of diamonds', 'diamonds', 7),
('eight of diamonds', 'diamonds', 8),
('nine of diamonds', 'diamonds', 9),
('ten of diamonds', 'diamonds', 10),
('jack of diamonds', 'diamonds', 10),
('queen of diamonds', 'diamonds', 10),
('king of diamonds', 'diamonds', 10),
('ace of hearts', 'hearts', (1, 11)),
('two of hearts', 'hearts', 2),
('three of hearts', 'hearts', 3),
('four of hearts', 'hearts', 4),
('five of hearts', 'hearts', 5),
('six of hearts', 'hearts', 6),
('seven of hearts', 'hearts', 7),
('eight of hearts', 'hearts', 8),
('nine of hearts', 'hearts', 9),
('ten of hearts', 'hearts', 10),
('jack of hearts', 'hearts', 10),
('queen of hearts', 'hearts', 10),
('king of hearts', 'hearts', 10)
]

class DeckManager:
	# will be used as stack
	deck = []
	def draw(self):
		if len(self.deck) == 0:
			print('rebuilding deck...')
			self.build_deck()
		return self.deck.pop()

	def build_deck(self):
		temp_all_cards = all_cards.copy()
		for x in range(52):
			next_card_index = random.randint(0,len(temp_all_cards)-1)
			card = temp_all_cards.pop(next_card_index)
			self.deck.append(Card(card[1], card[0], card[2]))

	def __init__(self):
		self.build_deck()

