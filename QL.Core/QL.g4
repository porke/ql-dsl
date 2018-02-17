grammar QL;

/*
*  Parser Rules
*/

form: FORM LABEL LCB (statements)? RCB;

statements: question (statements)?
		  | IF expresion LCB (statements)? RCB (ELSE LCB (statements)? RCB)?;

question: description name type (ASSIGNMENT expresion)?;

expresion: LB expresion RB
         | LABEL
		 | literal
		 | unOp expresion
		 | expresion binOp expresion;

description: STR;
name: LABEL COLON;
type: TYPEBOOL | TYPEINT | TYPEDEC | TYPESTR | TYPEDATE | TYPEMONEY;

literal: BOOL | INT | DEC | STR | MONEY | DATE;

binOp: GT | ST | GE | SE | NE | AND | OR | EQ | PLUS | MINUS | DIVIDE | MULTIPLY;
unOp: MINUS | PLUS | NOT;

/* 
*  Lexer Rules
*/

// Logical operators.
AND:  '&&';
OR:   '||';
NOT:  '!';
EQ:   '==';

// Operators for numerical comperison
GT: '>';
ST: '<';
GE: '>=';
SE: '<=';
NE: '!=';

// Operators for arithmetics
PLUS:     '+';
MINUS:    '-';
DIVIDE:   '/';
MULTIPLY: '*';

// Terms
FORM:	'form';
IF:		'if';
ELSE:	'else';

// Types
TYPEBOOL:   'boolean';
TYPEINT:    'integer';
TYPEDEC:    'decimal';
TYPESTR:    'string';
TYPEDATE:   'date';
TYPEMONEY:  'money';

LCB:		'{';
RCB:		'}';
LB:			'(';
RB:			')';
COLON:		':';
ASSIGNMENT: '=';

// Fragments
fragment UPPERCASE: ('A'..'Z');
fragment LOWERCASE: ('a'..'z');
fragment NUMBER:	('0'..'9');

// Literals
BOOL: ('true'|'false'); 
INT:  NUMBER+;
DEC:  INT '.' (NUMBER (NUMBER)?)?;
STR:  '"' .*? '"';
DATE: INT '-' INT '-' INT;
MONEY: INT '.' NUMBER NUMBER;

// Labels (Placed last, because it will match with other keywords)
LABEL:	(LOWERCASE|UPPERCASE)(LOWERCASE|UPPERCASE|NUMBER|'_')*;

// Hidden
WHITESPACE:	    (' ' | '\t' | '\n' | '\r' |) -> skip;
//MULTICOMMENT:   '/*' .* '*/' -> skip;
SINGLECOMMENT:  '//' ~[\r\n]* -> skip;