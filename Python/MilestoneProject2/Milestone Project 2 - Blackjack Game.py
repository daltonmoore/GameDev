# Milestone Project 2 - Blackjack Game
# In this milestone project you will be creating a Complete BlackJack Card Game in Python.

# Here are the requirements:

# You need to create a simple text-based [BlackJack](https://en.wikipedia.org/wiki/Blackjack) game
# The game needs to have one player versus an automated dealer.
# The player can stand or hit.
# The player must be able to pick their betting amount.
# You need to keep track of the player's total money.
# You need to alert the player of wins, losses, or busts, etc...

# And most importantly:

# You must use OOP and classes in some portion of your game. You can not just use functions in your game. Use classes to help you define the Deck and the Player's hand. 
# There are many right ways to do this, so explore it well!


# Feel free to expand this game. Try including multiple players. Try adding in Double-Down and card splits! Remember to you are free to use any resources you want and as always:

# HAVE FUN!
import os
from colorama import Fore, Back, Style, init
from Player import Player
from DeckManager import DeckManager

deckManager = DeckManager()
player = Player(500)
# computer is basically the house
computer = Player(0)
# maintains core game loop, is only set to false if
game_playing = True
out_of_chips = False
bet = 0

def game_over(playerLost, lossBy):
	global bet
	print('_________________________________________________')
	if playerLost:
		if lossBy == 'bust':
			print(f'{Fore.RED}Player busted')
		elif lossBy == 'beat':
			print(f'{Fore.RED}Player\'s hand was beat')
		elif lossBy == 'out':
			print(f'{Fore.RED}Player has no chips left')

	else:
		if lossBy == 'bust':
			player.add_chips(bet*2)
			print(f'{Fore.RED}Comptuer busted')

# draws the first couple cards
def draw_starting_cards(isPlayer):
	for x in range(2):
		if isPlayer:
			player.add_card(deckManager.draw())
		else:
			computer.add_card(deckManager.draw())


def print_player_chips():
	print(f'Your Chips: {player.chips}')


def setup_game():
	os.system('cls')
	print('Game Start...\n')
	# draw computer's starting cards
	draw_starting_cards(False)
	# draw player's starting cards
	draw_starting_cards(True)


def print_starting_hands():
	print(Fore.YELLOW +'Computer\'s hand:' )
	print(f'Cards: {Fore.WHITE}{computer}')
	print(f'{Fore.YELLOW}Hand total: {computer.total}')
	print('_________________________________________________')
	print(f'{Fore.WHITE}Player\'s hand:' )
	print(f'Cards: {player}')
	print(f'Hand total: {player.total}\n')


def print_players_hand():
	print('_________________________________________________')
	print('\nPlayer\'s hand:' )
	print(f'Cards: {player}')
	print(f'Hand total: {player.total}')


def print_computers_hand():
	print(Fore.YELLOW + '_________________________________________________')
	print('\nComputer\'s hand:' )
	print(f'Cards: {Fore.WHITE}{computer}')
	print(f'{Fore.YELLOW}Hand total: {computer.total}')


def player_choice():
	global game_playing

	while True:
		choice = input('Stay or Hit...').lower()
		if choice == 'hit' or choice == 'h':
			player.add_card(deckManager.draw())
		elif choice == 'stay' or choice == 's':
			break
		else:
			print('Invalid input\n')
			continue
		print_players_hand()

		if player.total > 21:
			game_playing = False
			game_over(True, 'bust') # player has lost
			break


def computer_turn():
	global game_playing
	global out_of_chips
	
	print_computers_hand()
	while True:
		if computer.total > player.total:
			game_playing = False
			game_over(True, 'beat')
			break
		else:
			computer.add_card(deckManager.draw())
		
		print_computers_hand()

		if computer.total > 21:
			game_playing = False
			game_over(False, 'bust')
			break


def player_bet():
	os.system('cls')
	global bet
	while True:
		print_player_chips()
		try:
			bet = int(input('How much to bet? '))
			if bet > player.chips:
				print(Fore.RED+'Enter smaller amount\n'+Fore.WHITE)
			elif bet < 1:
				print(Fore.RED+'Min bet is 1\n'+Fore.WHITE)
			else:
				player.chips -= int(bet)
				break
		except ValueError:
			print(Fore.RED+'Enter integer\n'+Fore.WHITE)


def reset():
	global game_playing
	game_playing = True
	player.reset_cards()
	computer.reset_cards()


def print_end_game_message():
	os.system('cls')
	print(f'You walk away with {player.chips} chips')


def main():
	global out_of_chips
	# core game loop
	os.system('cls') # just to clear for my benefit when I start up the script
	init() # initialize colorama
	continue_playing = True # to be used after game is over to keep playing

	while continue_playing:
		player_bet()
		setup_game()
		print_starting_hands()
		while game_playing:
			player_choice()
			if game_playing:
				computer_turn()
			if player.chips == 0:
				input('Press Enter to continue')
				out_of_chips = True
				continue_playing = False
				game_over(True, 'out')
		while not out_of_chips:
			continue_playing = input(f'{Fore.WHITE}Another game? ')
			if continue_playing.lower() == 'yes' or continue_playing == 'y':
				reset()
				break
			elif continue_playing.lower() == 'no' or continue_playing == 'n':
				continue_playing = False
				print_end_game_message()
				break
			else:
				print('Invalid input\n')


if __name__ == '__main__':
	main()