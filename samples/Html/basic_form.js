/* Style being applied to the iframe */

var customStyle = {

    // All the elements in the iframe
    // Position are from field + label

    iframe : {
        showCardTypeIcons : true,
        useTranslations : false,
        backgroundColor: 'white',
        width: '100%',
        height: 350,
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