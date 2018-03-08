grammar QLS;

stylesheet: STYLESHEET LABEL LEFTCURLY (page)* RIGHCURLY;
page: PAGE LABEL block;
block: LEFTCURLY RIGHTCURLY;

// Terms
STYLESHEET: 'stylesheet';
PAGE: 'page';


LEFTCURLY:		'{';
RIGHTCURLY:		'}';

// Fragments
fragment UPPERCASE: ('A'..'Z');
fragment LOWERCASE: ('a'..'z');
fragment NUMBER:	('0'..'9');

LABEL:	(LOWERCASE|UPPERCASE)(LOWERCASE|UPPERCASE|NUMBER|'_')*;

// Hidden
WHITESPACE:	    (' ' | '\t' | '\n' | '\r') -> skip;
SINGLECOMMENT:  '//' ~[\r\n]* -> skip;
