import {
  Checkbox,
  Input,
  Select,
  cn,
  type SelectProps,
} from '@nextui-org/react'
import {
  createContext,
  forwardRef,
  useContext,
  useId,
  type ComponentPropsWithoutRef,
  type ElementRef,
  type HTMLAttributes,
} from 'react'
import {
  Controller,
  FormProvider,
  useFormContext,
  type ControllerProps,
  type FieldPath,
  type FieldValues,
} from 'react-hook-form'

type FormFieldContextValue<
  TFieldValues extends FieldValues = FieldValues,
  TName extends FieldPath<TFieldValues> = FieldPath<TFieldValues>,
> = {
  name: TName
}

type FormItemContextValue = {
  id: string
}

const Form = FormProvider
const FormFieldContext = createContext<FormFieldContextValue>(
  {} as FormFieldContextValue,
)
const FormField = <
  TFieldValues extends FieldValues = FieldValues,
  TName extends FieldPath<TFieldValues> = FieldPath<TFieldValues>,
>({
  ...props
}: ControllerProps<TFieldValues, TName>) => {
  return (
    <FormFieldContext.Provider value={{ name: props.name }}>
      <Controller {...props} />
    </FormFieldContext.Provider>
  )
}

const FormItemContext = createContext<FormItemContextValue>(
  {} as FormItemContextValue,
)

const FormItem = forwardRef<HTMLDivElement, HTMLAttributes<HTMLDivElement>>(
  ({ className, ...props }, ref) => {
    const id = useId()

    return (
      <FormItemContext.Provider value={{ id }}>
        <div ref={ref} className={cn('space-y-2', className)} {...props} />
      </FormItemContext.Provider>
    )
  },
)
FormItem.displayName = 'FormItem'

const FormInput = forwardRef<
  ElementRef<typeof Input>,
  ComponentPropsWithoutRef<typeof Input>
>(({ ...props }, ref) => {
  const {
    error,
    formItemId,
    formDescriptionId,
    formMessageId,
    isTouched,
    name,
  } = useFormField()

  return (
    <Input
      ref={ref}
      id={formItemId}
      aria-describedby={
        !error
          ? `${formDescriptionId}`
          : `${formDescriptionId} ${formMessageId}`
      }
      aria-invalid={!!error}
      errorMessage={error?.message}
      isInvalid={error && isTouched}
      name={name}
      {...props}
    />
  )
})
FormInput.displayName = 'FormInput'

const FormCheckBox = forwardRef<
  ElementRef<typeof Checkbox>,
  ComponentPropsWithoutRef<typeof Checkbox>
>(({ ...props }, ref) => {
  const {
    error,
    formItemId,
    formDescriptionId,
    formMessageId,
    isTouched,
    name,
  } = useFormField()

  return (
    <Checkbox
      ref={ref}
      id={formItemId}
      aria-describedby={
        !error
          ? `${formDescriptionId}`
          : `${formDescriptionId} ${formMessageId}`
      }
      aria-invalid={!!error}
      errorMessage={error?.message}
      isInvalid={error && isTouched}
      name={name}
      radius='sm'
      {...props}
    >
      {props.children}
    </Checkbox>
  )
})

const FormSelect = forwardRef<HTMLDivElement, SelectProps>((props, ref) => {
  const {
    formDescriptionId,
    formItemId,
    formMessageId,
    name,
    isTouched,
    error,
  } = useFormField()
  return (
    <div ref={ref}>
      <Select
        id={formItemId}
        aria-describedby={
          !error
            ? `${formDescriptionId}`
            : `${formDescriptionId} ${formMessageId}`
        }
        aria-invalid={!!error}
        errorMessage={error?.message}
        isInvalid={error && isTouched}
        name={name}
        {...props}
      >
        {props.children}
      </Select>
    </div>
  )
})

const useFormField = () => {
  const fieldCtx = useContext(FormFieldContext)
  const itemCtx = useContext(FormItemContext)
  const { getFieldState, formState } = useFormContext()
  const fieldState = getFieldState(fieldCtx.name, formState)
  if (!fieldCtx) {
    throw new Error('useFormField should be used within <FormField>')
  }
  return {
    id: itemCtx.id,
    name: fieldCtx.name,
    formItemId: `${itemCtx.id}-form-item`,
    formDescriptionId: `${itemCtx.id}-form-item-description`,
    formMessageId: `${itemCtx.id}-form-item-message`,
    ...fieldState,
  }
}

export {
  Form,
  FormCheckBox,
  FormField,
  FormInput,
  FormItem,
  FormSelect,
  useFormField,
}
