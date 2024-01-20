import {
  Form,
  FormCheckBox,
  FormField,
  FormInput,
  FormItem,
} from '@/components/ui/form'
import { createLoginSchema, type LoginSchemaType } from '@/config/schema'
import useSignIn from '@/hooks/mutations/useSignIn'
import AuthLayout from '@/layouts/auth'
import { type NextPageWithLayout } from '@/pages/_app'
import { LINK_AUTH } from '@/utils/constants/links'
import { zodResolver } from '@hookform/resolvers/zod'
import {
  Button,
  CardBody,
  CardFooter,
  CardHeader,
  Image,
  Link,
  cn,
} from '@nextui-org/react'
import { capitalize } from 'lodash-es'
import { EyeIcon, EyeOffIcon } from 'lucide-react'
import { type GetServerSidePropsContext } from 'next'
import { useTranslation } from 'next-i18next'
import config from 'next-i18next.config.mjs'
import { serverSideTranslations } from 'next-i18next/serverSideTranslations'
import { useMemo, useState } from 'react'
import { useForm } from 'react-hook-form'

export async function getServerSideProps({
  locale,
}: GetServerSidePropsContext) {
  return {
    props: {
      ...(await serverSideTranslations(
        locale ?? 'vi',
        ['common', 'auth', 'error'],
        config,
      )),
    },

  }
}

const LoginScreen: NextPageWithLayout = () => {
  const { t } = useTranslation()
  const schema = useMemo(() => createLoginSchema(t), [t])
  const [TogglePassword, setTogglePassword] = useState(false)
  // ==================== React Hook Form ====================
  const form = useForm<LoginSchemaType>({
    mode: 'all',
    resolver: zodResolver(schema),
    defaultValues: {
      usernameOrEmail: '',
      password: '',
      isRemember: false,
    },
  })
  const { mutate: loginMutate, isPending } = useSignIn()

  const onSubmit = (data: LoginSchemaType) => loginMutate(data)

  return (
    <>
      <CardHeader className='flex h-1/3 flex-col items-center justify-between pt-5 sm:h-fit'>
        <h3 className='text-center text-3xl font-bold leading-8 text-secondary sm:leading-relaxed'>
          {cn(
            capitalize(t('common:experience')),
            t('common:and'),
            t('common:explore'),
          )}
          <br />
          Eco Farm
        </h3>
        <Image
          src='/assets/brands/logo.png'
          alt='logo'
          width={150}
          height={150}
        />
      </CardHeader>
      <CardBody className='justify-center'>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className='space-y-8'>
            <FormField
              control={form.control}
              name='usernameOrEmail'
              render={({ field }) => (
                <FormItem>
                  <FormInput
                    {...field}
                    type='text'
                    variant='flat'
                    isRequired
                    label={cn(
                      capitalize(t('common:username')),
                      t('common:or'),
                      t('auth:field.your-email'),
                    )}
                    autoComplete={'off'}
                  />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name='password'
              render={({ field }) => (
                <FormItem>
                  <FormInput
                    {...field}
                    type={TogglePassword ? 'text' : 'password'}
                    variant='flat'
                    isRequired
                    endContent={
                      <Button
                        variant='light'
                        isIconOnly
                        type='button'
                        onClick={() => setTogglePassword((prev) => !prev)}
                      >
                        {TogglePassword ? <EyeOffIcon /> : <EyeIcon />}
                      </Button>
                    }
                    label={capitalize(t('auth:field.your-password'))}
                  />
                </FormItem>
              )}
            />
            <div className='flex justify-between'>
              <FormField
                control={form.control}
                name='isRemember'
                render={({ field }) => (
                  <FormItem>
                    <FormCheckBox {...field} color={'primary'}>
                      {capitalize(t('auth:field.remember-me'))}
                    </FormCheckBox>
                  </FormItem>
                )}
              />
              <Link href={LINK_AUTH.FORGOT_PASSWORD} className='text-primary'>
                {capitalize(t('auth:field.forgot-password'))} ?
              </Link>
            </div>
          </form>
        </Form>
      </CardBody>
      <CardFooter className='flex-col gap-5 pb-7'>
        <Button
          onClick={form.handleSubmit(onSubmit)}
          color='primary'
          variant='solid'
          fullWidth
          className='font-bold'
          isLoading={isPending}
          disabled={isPending}
        >
          {capitalize(t('common:login'))}
        </Button>
        <div className='flex justify-center'>
          <span className='text-muted-foreground px-2 '>
            {capitalize(t('auth:not-have-account'))} ?
          </span>
          <Link href={LINK_AUTH.SIGN_UP} underline='hover' className=' '>
            {cn(capitalize(t('common:sign-up')), t('common:now'))}
          </Link>
        </div>
      </CardFooter>
    </>
  )
}
LoginScreen.getLayout = function getLayout(page) {
  return <AuthLayout>{page}</AuthLayout>
}

export default LoginScreen
