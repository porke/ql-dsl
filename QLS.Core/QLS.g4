grammar QLS;

stylesheet: STYLESHEET LABEL LEFTCURLY (page)* RIGHTCURLY;
page: PAGE LABEL LEFTCURLY (section)* RIGHTCURLY;
section: SECTION STRING LEFTCURLY (section_contents)* RIGHTCURLY;
question: QUESTION LABEL (widget)?
		| QUESTION LABEL LEFTCURLY (property)* RIGHTCURLY;
widget: WIDGET widget_type;

section_contents: question
				| style;

widget_type: WIDGETRADIO
		|	 WIDGETDROPDOWN
		|	WIDGETCHECKBOX
		|	WIDGETSPINBOX
		|	WIDGETTEXTBOX;

type: TYPEBOOLEAN
	| TYPEINTEGER
	| TYPEDECIMAL
	| TYPESTRING
	| TYPEDATE;

style: DEFAULT type LEFTCURLY (property)* RIGHTCURLY
	 | DEFAULT type widget;

property: LABEL COLON INTEGER	#integer_property
		 | LABEL COLON STRING	#string_property
		 | LABEL COLON COLOR	#color_property
		 | widget				#widget_property;

// Terms
STYLESHEET: 'stylesheet';
PAGE: 'page';
SECTION: 'section';
QUESTION: 'question';
WIDGET: 'widget';
DEFAULT: 'default';

LEFTCURLY:		'{';
RIGHTCURLY:		'}';
COLON:			':';

// Widgets
WIDGETRADIO:		'radio';
WIDGETDROPDOWN:		'dropdown';
WIDGETCHECKBOX:		'checkbox';
WIDGETSPINBOX:		'spinbox';
WIDGETTEXTBOX:		'textbox';

// Types
TYPEBOOLEAN:	'boolean';
TYPEINTEGER:    'integer';
TYPEDECIMAL:    'decimal';
TYPESTRING:		'string';
TYPEDATE:		'date';

// Fragments
fragment UPPERCASE: ('A'..'Z');
fragment LOWERCASE: ('a'..'z');
fragment NUMBER:	('0'..'9');

LABEL:	(LOWERCASE|UPPERCASE)(LOWERCASE|UPPERCASE|NUMBER|'_')*;
STRING:		'"' .*? '"';
INTEGER:	NUMBER+;
COLOR:	'#'NUMBER NUMBER NUMBER NUMBER NUMBER NUMBER;

// Hidden
WHITESPACE:	    (' ' | '\t' | '\n' | '\r') -> skip;
SINGLECOMMENT:  '//' ~[\r\n]* -> skip;
