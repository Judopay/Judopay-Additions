/* Style being applied to the iframe */

var customStyle = {

    // All the elements in the iframe
    // Position are from field + label

    iframe1 : {
        showCardTypeIcons : true,
        useTranslations : false,
        backgroundColor: '#e5e5e5',
        fields: {
            generic: {
                borderRadius: '',
                backgroundColor: 'white',
                backgroundColorFocus: '#e5ecff',
                backgroundColorError: '#ffe5e5',
                backgroundColorValid: '#e5ffec',
                textColor: 'black',
                textColorFocus: 'black',
                textColorError: 'red',
                textColorValid: 'green',
                labelTextColor: '#828282',
                labelTextColorError: 'red',
                labelTextColorValid: 'green',
                borderColor: '',
                border: '1px solid #b4b4b4',
                borderError: '1px solid #ff0000',
                borderStyle: 'solid',
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
    },


    iframe2 : {
        showCardTypeIcons : true,
        useTranslations : false,
        backgroundColor: '#525f7f',
        fields: {
            generic: {
                borderRadius: '20px',
                backgroundColor: '#7488aa',
                backgroundColorFocus: 'white',
                backgroundColorError: 'white',
                backgroundColorValid: 'lightgreen',
                textColor: 'black',
                textColorFocus: 'black',
                textColorError: 'black',
                textColorValid: '',
                labelTextColor: 'white',
                labelTextColorError: 'red',
                labelTextColorValid: '#828282',
                borderColor: '',
                border: '1px solid #b4b4b4',
                borderError: '1px solid #ff0000',
                borderStyle: 'solid',
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
                width: '30%',
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
                width: '30%',
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