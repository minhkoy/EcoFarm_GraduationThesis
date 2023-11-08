interface iProps {
    value: string;
    onClick: () => void;
    disabled?: boolean;
    htmlType?: 'button' | 'submit' | 'reset';
    style?: React.CSSProperties;
    className?: string;

}

const Button = (props: iProps) => {
    let renderButtonByType = () => {
        switch (props.htmlType) {
            case 'button': return (
                <>
                    <button type="button" 
                    disabled={props.disabled}
                    className={`inline-flex items-center px-4 py-2 border border-transparent text-base font-medium rounded-md shadow-sm text-white bg-green-600 hover:bg-green-500 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 ${props.className}`}
                    style={props.style}
                    onClick={props.onClick}
                    >
                        {props.value}
                    </button>
                </>
            )
        }
    }
}

export default Button