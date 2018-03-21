grammar QLS;

stylesheet: STYLESHEET LABEL LEFTCURLY (page)* RIGHTCURLY;
page: PAGE LABEL LEFTCURLY (section)* RIGHTCURLY;
section: SECTION STRING LEFTCURLY (section_contents)* RIGHTCURLY;
question: QUESTION LABEL (style)?;		
widget: WIDGET widget_type;

section_contents: question
				| default_style;

widget_type: option_widget LEFTBRACKET option COMMA option RIGHTBRACKET		
		|	WIDGETSPINBOX
		|	WIDGETTEXTBOX
		| WIDGETCHECKBOX;

option_widget: WIDGETRADIO 
			   | WIDGETDROPDOWN;

option: STRING;

type: TYPEBOOLEAN
	| TYPEINTEGER
	| TYPEDECIMAL
	| TYPESTRING
	| TYPEDATE;

style: LEFTCURLY (property)* RIGHTCURLY
	  | widget;

default_style: DEFAULT type style;	 

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
LEFTBRACKET:	'(';
RIGHTBRACKET:	')';
COLON:			':';
COMMA:			',';

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
fragment HEX_DIGIT:	('a'..'f');

LABEL:	(LOWERCASE|UPPERCASE)(LOWERCASE|UPPERCASE|NUMBER|'_')*;
STRING:		'"' .*? '"';
INTEGER:	NUMBER+;
COLOR:	'#'(HEX_DIGIT|NUMBER) (HEX_DIGIT|NUMBER) (HEX_DIGIT|NUMBER) (HEX_DIGIT|NUMBER) (HEX_DIGIT|NUMBER) (HEX_DIGIT|NUMBER);

// Hidden
WHITESPACE:	    (' ' | '\t' | '\n' | '\r') -> skip;
SINGLECOMMENT:  '//' ~[\r\n]* -> skip;
