grammar QLS;

stylesheet: STYLESHEET LABEL LEFTCURLY (page)* RIGHCURLY;
page: PAGE LABEL LEFTCURLY (section)* RIGHCURLY;
section: SECTION STRING LEFTCURLY (question)* RIGHCURLY;
question: QUESTION LABEL;

// Terms
STYLESHEET: 'stylesheet';
PAGE: 'page';
SECTION: 'section';
QUESTION: 'question';


LEFTCURLY:		'{';
RIGHTCURLY:		'}';

// Fragments
fragment UPPERCASE: ('A'..'Z');
fragment LOWERCASE: ('a'..'z');
fragment NUMBER:	('0'..'9');

LABEL:	(LOWERCASE|UPPERCASE)(LOWERCASE|UPPERCASE|NUMBER|'_')*;
STRING:		'"' .*? '"';

// Hidden
WHITESPACE:	    (' ' | '\t' | '\n' | '\r') -> skip;
SINGLECOMMENT:  '//' ~[\r\n]* -> skip;
