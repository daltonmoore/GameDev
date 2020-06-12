import os

class InvalidPosition(Exception):
	pass
class NoWin(Exception):
	pass

board = [
	[0,0,0],
	[0,0,0],
	[0,0,0]
	]

board_dict = {
	'9':(0,2),
	'8':(0,1),
	'7':(0,0),
	'6':(1,2),
	'5':(1,1),
	'4':(1,0),
	'3':(2,2),
	'2':(2,1),
	'1':(2,0)
	}

winner = '' #the winner to be printed out
pTurn = 'one' #which player's turn it is
row = -1 #global var for selected row position to place x or o
col = -1 #global var for selected col position to place x or o


def clear_screen():
	os.system('cls')

def print_board():
	clear_screen()
	board_output = '\n\n'
	for r, row in enumerate(board):
		board_output += '\t '
		for c, col in enumerate(row):
			if col == 0:
				board_output += ' '
			elif col == 1:
				board_output += 'x'
			elif col == 2:
				board_output += 'o'

			if c <2:
				board_output += ' | '

		board_output += '\t\n'
		if r < 2:
			board_output += '\t___|___|___\n'
		else:
			board_output += '\t   |   |   \n'

	print(board_output)

#check if player's selected position is open
def check_pos_valid():
	if (board[row][col] == 0):
		return True
	raise InvalidPosition()

def place_piece():
	if (pTurn == 'one'):
		board[row][col] = 1
	else:
		board[row][col] = 2

def toggle_turn():
	global pTurn
	if (pTurn == 'one'):
		pTurn = 'two'
	else:
		pTurn = 'one'

def calc_win():
	colWin = {0:0, 1:0, 2:0}
	rowWin = 0
	diagRightWin = 0
	diagLeftWin = 0

	for r, row in enumerate(board):
		for c, col in enumerate(row):
			if (col == 1): # x check
				rowWin += 1
				colWin[c] += 1
				if c == r:
					diagRightWin += 1
			elif (col == 2): # o check
				rowWin -= 1
				colWin[c] -= 1
				if c == 3:
					diagRightWin -= 1

		if (rowWin == 3 or diagRightWin == 3):
			return 'x'
		elif (rowWin == -3 or diagRightWin == -3):
			return 'o'

	if colWin[0] == 3 or colWin[1] == 3 or colWin[2] == 3:
		return 'x'
	elif colWin[0] == -3 or colWin[1] == -3 or colWin[2] == -3:
		return 'o'


	if (board[0][2] == 1 and
	board[1][1] == 1 and
	board[2][0] == 1):
		return 'x'
	if (board[0][2] == 2 and
	board[1][1] == 2 and
	board[2][0] == 2):
		return 'o'

def check_draw():
	for row in board:
		for col in row:
			if col == 0:
				return False
	return True
	
def print_win(winner):
	print_board()
	print(winner + ' is the winner')

def print_draw():
	print_board()
	print('Draw')

def check_win():
	winner = calc_win()
	if winner == 'x' or winner == 'o':
		print_win(winner)
	elif check_draw():
		print_draw()
	else:
		raise NoWin()

def print_instructions():
	print('Pick position to place piece, with each number on the numpad representing a cell in the grid\n')
	print('It is player ' + pTurn + '\'' + 's turn')

def parse_input(pos):
	global row
	global col
	pair = board_dict[pos]
	row = pair[0]
	col = pair[1]

def check_continue_playing():
	answer = input('Play another game?')
	if answer.lower() == 'yes':
		return True
	else:
		return False

def reset_board():
	global board 
	board = [
		[0,0,0],
		[0,0,0],
		[0,0,0]
		]
	raise Exception

def core_game_loop():
	while True:
		print_board()
		print_instructions()
		while True:
			pos = input('awaiting input...')
			try:
				parse_input(pos)
				if (check_pos_valid()):
					place_piece()
					toggle_turn()
					break
			except InvalidPosition:
				print('Invalid position chosen, pick another\n')
			except Exception:
				print('Invalid Input\n')
		try:
			check_win()
			if check_continue_playing():
				reset_board()
			break
		except NoWin as e:
			pass
		except Exception:
			pass

def main():
	core_game_loop()

if __name__ == "__main__":
	main()
