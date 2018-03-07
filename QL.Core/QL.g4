grammar QL;

/*
*  Parser Rules
*/

form: FORM LABEL block;

block: LEFTCURLY (statement)* RIGHTCURLY;

statement: question
		 | conditional;

question: STRING LABEL COLON type (ASSIGNMENT expression)?;

conditional: IF expression ifBlock (elseBlock)?;

ifBlock: block;

elseBlock: ELSE block;

expression: LEFTBRACKET expression RIGHTBRACKET		#scopedExpresion
			| LABEL									#variableExpression
			| literal								#literalExpression
			| unaryOperator expression				#unaryExpression
			| expression binaryOperator expression	#binaryExpression;

type: TYPEBOOLEAN
	| TYPEINTEGER
	| TYPEDECIMAL
	| TYPESTRING
	| TYPEDATE;

literal: BOOLEAN 
		| INTEGER 
		| DECIMAL 
		| STRING
		| DATE;

binaryOperator: GREATERTHAN 
				| SMALLERTHAN 
				| GREATEREQUAL 
				| SMALLEREQUAL 
				| NOTEQUAL 
				| AND 
				| OR 
				| EQUAL 
				| PLUS 
				| MINUS 
				| DIVIDE	
				| MULTIPLY;

unaryOperator: MINUS 
				| PLUS 
				| NOT;

/* 
*  Lexer Rules
*/

// Logical operators.
AND:	'&&';
OR:		'||';
NOT:	'!';

// Operators for comperison
GREATERTHAN:	'>';
SMALLERTHAN:	'<';
GREATEREQUAL:	'>=';
SMALLEREQUAL:	'<=';
NOTEQUAL:		'!=';
EQUAL:			'==';

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
TYPEBOOLEAN:	'boolean';
TYPEINTEGER:    'integer';
TYPEDECIMAL:    'decimal';
TYPESTRING:		'string';
TYPEDATE:		'date';

LEFTCURLY:		'{';
RIGHTCURLY:		'}';
LEFTBRACKET:	'(';
RIGHTBRACKET:	')';
COLON:			':';
ASSIGNMENT:		'=';

// Fragments
fragment UPPERCASE: ('A'..'Z');
fragment LOWERCASE: ('a'..'z');
fragment NUMBER:	('0'..'9');

// Literals
BOOLEAN:	('true'|'false'); 
INTEGER:	NUMBER+;
DECIMAL:	INTEGER '.' INTEGER;
STRING:		'"' .*? '"';
DATE:		INTEGER '-' INTEGER '-' INTEGER;

// Labels (Placed last, because it will match with other keywords)
LABEL:	(LOWERCASE|UPPERCASE)(LOWERCASE|UPPERCASE|NUMBER|'_')*;

// Hidden
WHITESPACE:	    (' ' | '\t' | '\n' | '\r') -> skip;
SINGLECOMMENT:  '//' ~[\r\n]* -> skip;