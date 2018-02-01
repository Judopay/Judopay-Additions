/* Style being applied to the iframe */

var customStyle = {

    // All the elements in the iframe
    // Position are from field + label

    iframe1 : {
        showCardTypeIcons : true,
        useTranslations : true,
        backgroundColor: 'white',
        width: '100%',
        height: 350,
		errorTranslations: {
			0: "El número de la tarjeta de crédito solo puede contener números",
			1: "El número de la tarjeta de crédito no es válido",
			2: "Tipo de tarjeta de crédito no reconocido. Por favor, vuelva a comprobar su número",
			3: "Se requiere un tipo de tarjeta de crédito",
			4: "La tarjeta de crédito ha caducado",
			5: "La fecha de caducidad de la tarjeta de crédito no es válida",
			6: "Fecha de caducidad requerida",
			7: "Código {0} demasiado corto para la {1}",
			8: "Código de {0} demasiado largo para la {1}",
			9: "El código {0} solo puede contener números",
			10: "Código de {0} requerido para la tarjeta {1}"
		},
		placeholderTranslations: {
			"cardNumberPlaceholderText": "Número de tarjeta de crédito",
			"cvcPlaceholderText": "CVC",
			"expiryPlaceholderText": "Fecha de caducidad"
		},
        fields: {
            generic: {
                borderRadius: '',
                backgroundColor: 'white',
                backgroundColorFocus: 'white',
                backgroundColorError: 'white',
                backgroundColorValid: 'white',
                textColor: 'black',
                textColorFocus: 'black',
                textColorError: 'red',
                textColorValid: '',
                labelTextColor: '#828282',
                labelTextColorError: 'red',
                labelTextColorValid: '#828282',
                border: '1px solid #b4b4b4',
                borderError: '1px solid #ff0000',
                width: '100%'
            },
            // Field properties will take priority if defined
            cardNumber: {
                placeholder: 'Card Number',
                x: '',
                y: '', // eg 250px
                width: '',
                height: '',
                borderRadius: '',
                backgroundColor: '',
                backgroundColorFocus: '',
                backgroundColorError: '',
                backgroundColorValid: '',
                textColor: '',
                textColorFocus: '',
                textColorError: '',
                textColorValid: '',
                labelTextColor: '',
                labelTextColorError: '',
                labelTextColorValid: '',
                borderColor: ''
            },
            expiryDate: {
                placeholder: 'MM/YY',
                x: '',
                y: '',
                width: '',
                height: '',
                borderRadius: '',
                backgroundColor: '',
                backgroundColorFocus: '',
                backgroundColorError: '',
                backgroundColorValid: '',
                textColor: '',
                textColorFocus: '',
                textColorError: '',
                textColorValid: '',
                labelTextColor: '',
                labelTextColorError: '',
                labelTextColorValid: '',
                borderColor: ''
            },
            cv2: {
                placeholder: 'CVC',
                x: '',
                y: '',
                width: '',
                height: '',
                borderRadius: '',
                backgroundColor: '',
                backgroundColorFocus: '',
                backgroundColorError: '',
                backgroundColorValid: '',
                textColor: '',
                textColorFocus: '',
                textColorError: '',
                textColorValid: '',
                labelTextColor: '',
                labelTextColorError: '',
                labelTextColorValid: '',
                borderColor: ''
            }
        }		
    }
};