type FormFieldProps = {
    label: string;
    onFieldChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
} & React.InputHTMLAttributes<HTMLInputElement>;

export default FormFieldProps;