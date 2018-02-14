grammar QL;

/*
*  Parser Rules
*/

form: FORM LABEL BEGIN (statements)? END;

statements: question (statements)?
		  | IF expresion BEGIN (statements)? END (ELSE BEGIN (statements)? END)?;


question: description name type (ASSIGNMENT expresion)?;

expresion: LB expresion RB
         | LABEL
		 | literal
		 | unOp expresion
		 | expresion binOp expresion;

description: STR;

name: LABEL COLON;

type: TYPEBOOL | TYPEINT | TYPEDEC | TYPESTR | TYPEDATE | TYPEMONEY;

literal: BOOL | INT | DEC | STRING | MONEY; // | DATE

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

// Terms and labels
FORM:	'form';
IF:		'if';
ELSE:	'else';
LABEL:	('a'..'z'|'A'..'Z')('a'..'z'|'A'..'Z'|'0'..'9'|'_')*;

// Types
TYPEBOOL:   'boolean';
TYPEINT:    'integer';
TYPEDEC:    'decimal';
TYPESTR:    'string';
TYPEDATE:   'date';
TYPEMONEY:  'money';

BEGIN:		'{';
END:		'}';
LB:			'(';
RB:			')';
COLON:		':';
ASSIGNMENT: '=';

// Literals
BOOL: ('true'|'false');
INT:  ('0'..'9')+;
DEC:  ('0'..'9')+ (.[0-9][0-9]?)?;
STR:  '"' .*? '"';
//DATE: 
MONEY: ('0'..'9')+ '.' ('0'..'9') ('0'..'9');

// Hidden
WHITESPACE:	    (' ' | '\t' | '\n' | '\r') -> skip;
//MULTICOMMENT:   '/*' .* '*/' -> skip;
SINGLECOMMENT:  '//' ~[\r\n]* -> skip;