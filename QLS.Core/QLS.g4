grammar QLS;

stylesheet: STYLESHEET LABEL LEFTCURLY (page)* RIGHTCURLY;
page: PAGE LABEL LEFTCURLY (section)* RIGHTCURLY;
section: SECTION STRING LEFTCURLY (question)* RIGHTCURLY;
question: QUESTION LABEL (widget)?;
widget: WIDGET widget_type;

widget_type: WIDGETRADIO
		|	 WIDGETDROPDOWN
		|	WIDGETCHECKBOX;

// Terms
STYLESHEET: 'stylesheet';
PAGE: 'page';
SECTION: 'section';
QUESTION: 'question';
WIDGET: 'widget';

LEFTCURLY:		'{';
RIGHTCURLY:		'}';

// Widgets
WIDGETRADIO:		'radio';
WIDGETDROPDOWN:		'dropdown';
WIDGETCHECKBOX:		'checkbox';

// Fragments
fragment UPPERCASE: ('A'..'Z');
fragment LOWERCASE: ('a'..'z');
fragment NUMBER:	('0'..'9');

LABEL:	(LOWERCASE|UPPERCASE)(LOWERCASE|UPPERCASE|NUMBER|'_')*;
STRING:		'"' .*? '"';

// Hidden
WHITESPACE:	    (' ' | '\t' | '\n' | '\r') -> skip;
SINGLECOMMENT:  '//' ~[\r\n]* -> skip;
