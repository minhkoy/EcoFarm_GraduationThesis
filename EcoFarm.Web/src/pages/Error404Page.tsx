export default function Error404Page() {
    return (
      <div className="flex h-screen flex-col justify-center px-6 py-12 lg:px-8 bg-bg-login bg-cover bg-no-repeat">
        <div
        style={{
          display: "flex",
          alignItems: "center",
          flexDirection: "column",
        }}
        >
          <h1 style={{ fontSize: 56, color: 'red', fontWeight: 'bold' }}>Oops!</h1>
          <h1 className="p text-lg font-bold">Không tìm thấy trang web này</h1>
          <h2 className="text-white"> Có thể bạn nhầm ở đâu đó. Vui lòng kiểm tra lại đường dẫn và thử lại.</h2>
          <a href="/">
            <h3 style={{ cursor: "pointer", textDecoration: "underline" }}>
              Trở về trang chủ
            </h3>
          </a>
        </div>
      </div>
    )
}